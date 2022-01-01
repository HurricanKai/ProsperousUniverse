using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class SystemByIdDataLoader : CacheDataLoader<string, SystemDTO>
{
    private readonly DataCache _dataCache;

    public SystemByIdDataLoader(
        DataCache dataCache,
        DataLoaderOptions? options = null) : base(options)
    {
        _dataCache = dataCache;
    }

    protected override async Task<SystemDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => SystemDTO.Parse(await _dataCache.GetData("systems/" + key));
}
