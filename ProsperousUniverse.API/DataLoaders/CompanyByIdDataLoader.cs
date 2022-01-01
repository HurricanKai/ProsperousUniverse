using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class CompanyByIdDataLoader : CacheDataLoader<string, CompanyDTO>
{
    private readonly DataCache _dataCache;

    public CompanyByIdDataLoader(
        DataCache dataCache,
        DataLoaderOptions? options = null) : base(options)
    {
        _dataCache = dataCache;
    }

    protected override async Task<CompanyDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => CompanyDTO.Parse(await _dataCache.GetData("companies/" + key));
}
