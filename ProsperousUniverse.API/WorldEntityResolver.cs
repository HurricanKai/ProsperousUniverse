using ProsperousUniverse.API.DataLoaders;
using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API;

public sealed class WorldEntityResolver
{
    private readonly PlanetByIdDataLoader _planetByIdDataLoader;
    private readonly SystemByIdDataLoader _systemByIdDataLoader;

    public WorldEntityResolver(PlanetByIdDataLoader planetByIdDataLoader, SystemByIdDataLoader systemByIdDataLoader)
    {
        _planetByIdDataLoader = planetByIdDataLoader;
        _systemByIdDataLoader = systemByIdDataLoader;
    }
    
    public async Task<IWorldEntityDTO> GetWorldEntity(string type, string id)
    {
        return type switch
        {
            "PLANET" => await _planetByIdDataLoader.LoadAsync(id),
            "SYSTEM" => await _systemByIdDataLoader.LoadAsync(id),
            _ => FallbackEntity(type)
        };
    }

    private static IWorldEntityDTO FallbackEntity(string type)
    {
        throw new InvalidOperationException($"Unknown Type \"{type}\"");
    }
}
