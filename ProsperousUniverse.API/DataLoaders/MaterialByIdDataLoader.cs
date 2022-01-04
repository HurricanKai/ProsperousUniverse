using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class MaterialByIdDataLoader : CacheDataLoader<string, MaterialDTO>
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public MaterialByIdDataLoader(IServerInterface serverInterface, IFusionCache cache) : base(new DataLoaderOptions())
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }

    protected override async Task<MaterialDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => await _cache.GetOrSetAsync("PU_MATERIAL_ID" + key, async c  =>
        {
            var e = await _serverInterface.DoAction(
                    x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldFindMaterialData,
                        new { query = key, actionId = x }, "world-data")), c);
            Debug.Assert(e.MessageType == ActionNames.WorldMaterialData);
            return MaterialDTO.Parse(e.Payload);
        }, token: cancellationToken);
}
