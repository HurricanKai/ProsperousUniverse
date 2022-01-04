using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API;

public sealed class MaterialCategories
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public MaterialCategories(IServerInterface serverInterface, IFusionCache cache)
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }

    public async Task<IEnumerable<MaterialCategoryDTO>> GetCategories()
    {
        return await _cache.GetOrSetAsync("PU_WORLD_DATA_MATERIAL_CATEGORIES", async c 
            =>
        {
            var e = await _serverInterface.DoAction(
                x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldGetMaterialCategories,
                    new { actionId = x }, "world-data")), c);
            Debug.Assert(e!.MessageType == ActionNames.WorldMaterialCategories);
            return e.Payload.GetProperty("categories").EnumerateArray()
                .Select(MaterialCategoryDTO.Parse)
                .ToArray();
        });
    }
}
