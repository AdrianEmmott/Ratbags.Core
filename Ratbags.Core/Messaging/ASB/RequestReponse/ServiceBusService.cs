using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Ratbags.Core.Messaging.ASB.RequestReponse;

public abstract class ServiceBusService<TServiceBusType> : IAsyncDisposable
{
    private readonly ServiceBusClient _sbClient;
    private ServiceBusProcessor? _processor;

    private readonly ConcurrentDictionary<string, TaskCompletionSource<ServiceBusReceivedMessage>> _pending = new();

    private readonly string _responseTopic;
    private readonly string _responseSubscription;

    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
    private readonly ILogger<TServiceBusType> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ServiceBusService(
        ServiceBusClient sbClient,
        ILogger<TServiceBusType> logger,
        IOptions<JsonSerializerOptions> jsonOptions,
        string responseTopic,
        string responseSubscription)
    {
        _sbClient = sbClient;
        _logger = logger;
        _responseTopic = responseTopic;
        _responseSubscription = responseSubscription;

        // start response listener once
        var processor = _sbClient.CreateProcessor(
            _responseTopic,
            _responseSubscription,
            new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 10
            });

        _processor = processor;

        processor.ProcessMessageAsync += OnResponseMessageAsync;
        processor.ProcessErrorAsync += args =>
        {
            _logger.LogError(args.Exception, "ServiceBus processor error");
            return Task.CompletedTask;
        };

        _ = Task.Run(async () =>
        {
            try { await processor.StartProcessingAsync(); }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to start ServiceBus processor");
            }
        });

        _jsonOptions = jsonOptions.Value;
    }

    public async Task<TResponse?> SendRequestAsync<TRequest, TResponse>(
        TRequest request,
        string requestTopic,
        TimeSpan? timeout = null,
        CancellationToken ct = default)
    {
        var correlationId = Guid.NewGuid().ToString();

        var tcs = new TaskCompletionSource<ServiceBusReceivedMessage>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        if (!_pending.TryAdd(correlationId, tcs))
        {
            throw new InvalidOperationException("Duplicate correlationId (should never happen).");
        }

        try
        {
            await using var sender = _sbClient.CreateSender(requestTopic);

            var requestMessage = CreateRequestMessage(request, correlationId, _responseTopic);

            await sender.SendMessageAsync(requestMessage, ct);

            var effectiveTimeout = timeout ?? DefaultTimeout;

            ServiceBusReceivedMessage responseMessage;

            try
            {
                responseMessage = await tcs.Task.WaitAsync(effectiveTimeout, ct);
            }
            catch (TimeoutException)
            {
                return default;
            }

            var bodyString = responseMessage.Body.ToString();

            return JsonSerializer.Deserialize<TResponse>(bodyString, _jsonOptions);
        }
        finally
        {
            _pending.TryRemove(correlationId, out _);
        }
    }

    private ServiceBusMessage CreateRequestMessage<T>(T request, string correlationId, string replyTo)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        var requestMessage = new ServiceBusMessage(BinaryData.FromString(json))
        {
            CorrelationId = correlationId,
            ReplyTo = replyTo,
            ContentType = "application/json"
        };

        return requestMessage;
    }

    private async Task OnResponseMessageAsync(ProcessMessageEventArgs args)
    {
        var msg = args.Message;

        if (!string.IsNullOrWhiteSpace(msg.CorrelationId) &&
            _pending.TryRemove(msg.CorrelationId, out var tcs))
        {
            tcs.TrySetResult(msg);
            await args.CompleteMessageAsync(msg);
            return;
        }

        _logger.LogWarning("No pending request for correlationId {CorrelationId}. Completing.", msg.CorrelationId);
        await args.CompleteMessageAsync(msg);
    }

    public async ValueTask DisposeAsync()
    {
        var processor = Interlocked.Exchange(ref _processor, null);

        if (processor != null)
        {
            await processor.StopProcessingAsync();
            await processor.DisposeAsync();
        }
    }
}