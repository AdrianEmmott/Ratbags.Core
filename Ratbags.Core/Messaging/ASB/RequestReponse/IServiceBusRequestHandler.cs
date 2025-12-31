namespace Ratbags.Core.Messaging.ASB.RequestReponse;

public interface IServiceBusRequestHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct);
}