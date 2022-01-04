using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API;

public sealed class CountryRegistry
{
    private readonly IFusionCache _cache;
    private readonly IServerInterface _serverInterface;

    public CountryRegistry(IFusionCache cache, IServerInterface serverInterface)
    {
        _cache = cache;
        _serverInterface = serverInterface;
    }
    
    public async Task<IEnumerable<CountryDTO>> GetCountries()
    {
        return await _cache.GetOrSetAsync("PU_COUNTRY_REGISTRY", async entry
            =>
        {
            var e = await _serverInterface.DoAction(
                c => _serverInterface.SendMessage(new BaseMessage(ActionNames.CountryRegistryGetCountries,
                    new { actionId = c }, "country-registry")), entry);
            Debug.Assert(e.MessageType == ActionNames.CountryRegistryCountries);
            return e.Payload.GetProperty("countries").EnumerateArray().Select(CountryDTO.Parse).ToArray();
        });
    }

    private string ThrowRequiredPropertyMissing(string name)
    {
        throw new InvalidOperationException($"Required property missing: {name}");
    }
}
