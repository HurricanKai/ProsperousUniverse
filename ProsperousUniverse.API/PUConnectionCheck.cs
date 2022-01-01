using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProsperousUniverse.API.Interfaces;

namespace ProsperousUniverse.API;

public sealed class PUConnectionCheck : IHealthCheck
{
    private readonly IServerInterface _serverInterface;
    public PUConnectionCheck(IServerInterface serverInterface)
    {
        _serverInterface = serverInterface;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(_serverInterface.SocketState switch
        {
            SocketState.Offline => HealthCheckResult.Unhealthy("No socket connection"),
            SocketState.Connecting => HealthCheckResult.Degraded("Socket connecting..."),
            SocketState.Authenticating => HealthCheckResult.Degraded("Socket authenticating..."),
            SocketState.PostAuth => HealthCheckResult.Healthy("Socket connected & authenticated"),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}
