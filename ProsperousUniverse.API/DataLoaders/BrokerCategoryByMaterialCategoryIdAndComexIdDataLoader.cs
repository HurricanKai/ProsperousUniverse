using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader : CacheDataLoader<(string MaterialCategoryId, string ComexId), BrokerCategoryDTO>
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _fusionCache;

    public BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader(IServerInterface serverInterface, IFusionCache fusionCache)
    {
        _serverInterface = serverInterface;
        _fusionCache = fusionCache;
    }

    protected override async Task<BrokerCategoryDTO>
        LoadSingleAsync((string MaterialCategoryId, string ComexId) key, CancellationToken cancellationToken)
        => await _fusionCache.GetOrSetAsync("PU_BROKER_CATEGORY" + key.MaterialCategoryId + key.ComexId,
            async c =>
            {
                var e = await _serverInterface.DoAction(
                    x => _serverInterface.SendMessage(new BaseMessage(ActionNames.ComexExchangeGetBrokerList,
                        new { actionId = x, categoryId = key.MaterialCategoryId }, key.ComexId)), c);
                Debug.Assert(e.MessageType == ActionNames.ComexExchangeBrokerList);
                return BrokerCategoryDTO.Parse(e.Payload);
            }, token: cancellationToken);
}
