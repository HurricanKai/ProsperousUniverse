using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("System"), Node]
[Authorize(Policy = "read:pu_system")]
public class SystemDTO : IWorldEntityDTO, INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }
    
    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string NaturalId { get; set; }
    
    [GraphQLIgnore] public string? NamerId { get; set; }

    [GraphQLName("namer")]
    public Task<UserDTO?> GetNamerAsync([Service] UserByIdDataLoader userByIdDataLoader)
        => NamerId is null ? Task.FromResult<UserDTO>(null) : userByIdDataLoader.LoadAsync(NamerId);

    public DateTime NamingDate { get; set; }

    public bool Nameable { get; set; }

    [GraphQLNonNullType]
    public AddressDTO Address { get; set; }

    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }

    [GraphQLIgnore]
    public string CountryId { get; set; }

    [GraphQLName("country")]
    public Task<CountryDTO> GetCountryAsync([Service] CountryByIdDataLoader countryByIdDataLoader)
        => countryByIdDataLoader.LoadAsync(CountryId);
    
    [GraphQLIgnore]
    public string[] PlanetIds { get; set; }

    [GraphQLName("planets")]
    public async IAsyncEnumerable<PlanetDTO> GetPlanetsAsync([Service] PlanetByIdDataLoader planetByIdDataLoader)
    {
        foreach (var id in PlanetIds)
            yield return await planetByIdDataLoader.LoadAsync(id);
    }

    // TODO: Celestial bodies
    
    [GraphQLIgnore]
    public string[] ConnectionIds { get; set; }

    [GraphQLName("connections")]
    public async IAsyncEnumerable<SystemDTO> GetConnectionsAsync([Service] SystemByIdDataLoader systemByIdDataLoader)
    {
        foreach(var id in ConnectionIds)
            yield return await systemByIdDataLoader.LoadAsync(id);
    }

    public static SystemDTO Parse(JsonElement jsonElement)
    {
        if (jsonElement.GetProperty("celestialBodies").GetArrayLength() > 0)
        {
            File.WriteAllText($"./{Guid.NewGuid()}-celestial-bodies.txt", jsonElement.GetRawText());
        }
        
        return new SystemDTO
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            NaturalId = jsonElement.GetProperty("naturalId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("naturalId"),
            NamerId = jsonElement.GetProperty("namer").GetProperty("id").GetString(),
            NamingDate = ParsingUtils.ParseUnixWrappedTimestamp(jsonElement.GetProperty("namingDate")),
            Nameable = jsonElement.GetProperty("nameable").GetBoolean(),
            Address = AddressDTO.Parse(jsonElement.GetProperty("address")),
            Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency")),
            CountryId = jsonElement.GetProperty("country").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("country.id"),
            PlanetIds = jsonElement.GetProperty("planets").EnumerateArray().Select(x => x.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("planets[?].id")).ToArray(),
            ConnectionIds = jsonElement.GetProperty("connections").EnumerateArray().Select(x => x.GetString()!).ToArray(),
        };
    }

    [NodeResolver]
    public static Task<SystemDTO> Get(string id, [Service] SystemByIdDataLoader systemByIdDataLoader)
        => systemByIdDataLoader.LoadAsync(id);
}
