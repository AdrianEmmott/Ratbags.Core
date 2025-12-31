using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ratbags.Core.Messaging.ASB.RequestReponse;

public abstract class ServiceBusRequestReplyWorker<TRequest, TResponse> : BackgroundService
{
    private readonly string _connectionString;
    private readonly string _topicName;
    private readonly string _subscriptionName;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ServiceBusRequestReplyWorker<TRequest, TResponse>> _logger;

    private ServiceBusClient? _client;
    private ServiceBusProcessor? _processor;

    protected ServiceBusRequestReplyWorker(
        string connectionString,
        string topicName,
        string subscriptionName,
        IServiceScopeFactory scopeFactory,
        ILogger<ServiceBusRequestReplyWorker<TRequest, TResponse>> logger)
    {
        _connectionString = connectionString;
        _topicName = topicName;
        _subscriptionName = subscriptionName;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Listener starting: Topic={Topic} Sub={Sub} Type={Type}",
            _topicName, _subscriptionName, GetType().Name);

        _client = new ServiceBusClient(_connectionString);

        _processor = _client.CreateProcessor(_topicName, _subscriptionName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 5
        });

        _processor.ProcessMessageAsync += ProcessMessageAsync;
        _processor.ProcessErrorAsync += args =>
        {
            _logger.LogError(args.Exception, "Processor error. EntityPath={EntityPath}", args.EntityPath);
            return Task.CompletedTask;
        };

        await _processor.StartProcessingAsync(stoppingToken);

        // why does this keep running?
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
    {
        try
        {
            var request = JsonSerializer.Deserialize<TRequest>(args.Message.Body.ToString()
                , new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new InvalidOperationException("Request body deserialized to null.");

            TResponse response;
            using (var scope = _scopeFactory.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetRequiredService<IServiceBusRequestHandler<TRequest, TResponse>>();

                response = await handler.HandleAsync(request, args.CancellationToken);
            }

            await ReplyAsync(args, response);
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Message processing failed. Topic={Topic} Type={Type} DeliveryCount={DeliveryCount}",
                _topicName, GetType().Name, args.Message.DeliveryCount);

            await args.AbandonMessageAsync(args.Message);
        }
    }

    private async Task ReplyAsync(ProcessMessageEventArgs args, TResponse payload)
    {
        if (string.IsNullOrWhiteSpace(args.Message.ReplyTo)) return;

        var json = JsonSerializer.Serialize(payload);

        var response = new ServiceBusMessage(json)
        {
            CorrelationId = args.Message.CorrelationId,
            ContentType = "application/json"
        };

        await using var sender = _client!.CreateSender(args.Message.ReplyTo);
        await sender.SendMessageAsync(response);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_processor != null)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.DisposeAsync();
            _processor = null;
        }

        if (_client != null)
        {
            await _client.DisposeAsync();
            _client = null;
        }

        await base.StopAsync(cancellationToken);
    }
}
