using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API;

public sealed class CommodityExchangeList
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public CommodityExchangeList(IServerInterface serverInterface, IFusionCache cache)
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }
    
    public async Task<IEnumerable<ComexDTO>> GetCommodityExchanges()
    {
        return await _cache.GetOrSetAsync("PU_COMMODITY_EXCHANGE_LIST", x => _serverInterface.DoAction(
            x2 => _serverInterface.SendMessage(new BaseMessage(ActionNames.ComexGetExchangeList, new { actionId = x2 }, "comex-registry")), x =>
            {
                Debug.Assert(x.MessageType == ActionNames.ComexExchangeList);
                return x.Payload.GetProperty("exchanges").EnumerateArray().Select(ComexDTO.Parse).ToArray();
            }));
    }
}
