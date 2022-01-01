using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class CountryByIdDataLoader : BatchDataLoader<string, CountryDTO>
{
    private readonly CountryRegistry _countryRegistry;

    public CountryByIdDataLoader(
        CountryRegistry countryRegistry,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _countryRegistry = countryRegistry;
    }
    
    protected override async Task<IReadOnlyDictionary<string, CountryDTO>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
    {
        return (await _countryRegistry.GetCountries()).Where(x => keys.Contains(x.Id)).ToDictionary(x => x.Id);
    }
}
