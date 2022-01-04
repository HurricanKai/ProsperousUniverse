using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class BrokerByTickerDataLoader : CacheDataLoader<string, BrokerDTO>
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public BrokerByTickerDataLoader(IServerInterface serverInterface, IFusionCache cache)
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }

    protected override async Task<BrokerDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => await _cache.GetOrSetAsync("PU_BROKER" + key, c => _serverInterface.DoAction(
            x => _serverInterface.SendMessage(new BaseMessage(ActionNames.ComexGetBrokerData,
                new { actionId = x, ticker = key }, "comex-registry")), x =>
            {
                Debug.Assert(x.MessageType == ActionNames.ComexBrokerData);
                return BrokerDTO.Parse(x.Payload);
            }), token: cancellationToken);
}
