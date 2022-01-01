using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("Planet")]
[Authorize(Policy = "read:pu_planet")]
public sealed class PlanetDTO : IWorldEntityDTO, INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))] public string Id { get; set; }

    [GraphQLNonNullType] public string Name { get; set; }

    [GraphQLNonNullType] public string NaturalId { get; set; }

    [GraphQLIgnore] public string? NamerId { get; set; }

    [GraphQLName("namer")]
    public Task<UserDTO?> GetNamerAsync([Service] UserByIdDataLoader userByIdDataLoader)
        => NamerId is null ? Task.FromResult<UserDTO>(null) : userByIdDataLoader.LoadAsync(NamerId);

    public DateTime NamingDate { get; set; }

    public bool Nameable { get; set; }

    // TODO: Celestial Bodies

    [GraphQLNonNullType] public AddressDTO Address { get; set; }
    
    public double Gravity { get; set; }
    
    public double MagneticField { get; set; }
    
    public double Mass { get; set; }
    
    public double MassEarth { get; set; }

    [GraphQLNonNullType] public OrbitDTO Orbit { get; set; }
    public int OrbitIndex { get; set; }
    
    public double Pressure { get; set; }
    
    public double Radiation { get; set; }
    
    public double Radius { get; set; }

    [GraphQLNonNullType] public PlanetResourceDTO[] Resources { get; set; }
    public double Sunlight { get; set; }
    
    public bool Surface { get; set; }
    
    public double Temperature { get; set; }
    
    public int Plots { get; set; }
    
    public double Fertility { get; set; }

    [GraphQLNonNullType]
    public BuildOptionDTO[] BuildOptions { get; set; }  
    
    // TODO: Projects

    [GraphQLIgnore]
    public string? CountryId { get; set; }

    [GraphQLName("country")]
    public Task<CountryDTO> GetCountryAsync([Service] CountryByIdDataLoader countryByIdDataLoader)
        => countryByIdDataLoader.LoadAsync(CountryId);
    
    [GraphQLIgnore]
    public string? GovernorId { get; set; }

    [GraphQLName("governor")]
    public Task<UserDTO> GetGovernorAsync([Service] UserByIdDataLoader userByIdDataLoader)
        => userByIdDataLoader.LoadAsync(GovernorId);
    
    // TODO: Figure out what governing entities there are
    /* Example:
code "BGA"
id "0266894875ea91da4d948c9d2e330b04"
name "Boucher Governing Authority"
_proxy_key "0266894875ea91da4d948c9d2e330b04"
_type "corporation"
    */

    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }

    [GraphQLNonNullType]
    public PlanetLocalRulesDTO LocalRules { get; set; }
    
    [GraphQLIgnore]
    public string PopulationId { get; set; }

    [GraphQLName("population")]
    public Task<PopulationDTO> GetPopulationAsync([Service] PopulationByIdDataLoader populationByIdDataLoader)
        => populationByIdDataLoader.LoadAsync(PopulationId);
    
    [GraphQLIgnore]
    public string CogcId {get; set; }

    // TODO: Chamber of Global Commerce - I think the CogcId is always 000000000000000 and can just be gotten as part of the planet
    // but I am yet to figure out the overhead of that and/or the architectural problems.
    
    // [GraphQLName("cogc")]
    // public async Task<ChamberOfGlobalCommerceDTO> GetCogcAsync(
    //     [Service] ChamberOfGlobalCommerceByIdDataLoader chamberOfGlobalCommerceByIdDataLoader)
    //     => chamberOfGlobalCommerceByIdDataLoader.LoadAsync(CogcId);

    public static PlanetDTO Parse(JsonElement jsonElement)
    {
        if (jsonElement.GetProperty("celestialBodies").GetArrayLength() > 0)
        {
            File.WriteAllText($"./{Guid.NewGuid()}-celestial-bodies.txt", jsonElement.GetRawText());
        }

        var data = jsonElement.GetProperty("data");
        return new PlanetDTO
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            NaturalId = jsonElement.GetProperty("naturalId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("naturalId"),
            NamerId = jsonElement.GetProperty("namer").GetProperty("id").GetString(),
            NamingDate = ParsingUtils.ParseUnixWrappedTimestamp(jsonElement.GetProperty("namingDate")),
            Nameable = jsonElement.GetProperty("nameable").GetBoolean(),
            Address = AddressDTO.Parse(jsonElement.GetProperty("address")),
            Gravity = data.GetProperty("gravity").GetDouble(),
            MagneticField = data.GetProperty("magneticField").GetDouble(),
            Mass = data.GetProperty("mass").GetDouble(),
            MassEarth = data.GetProperty("massEarth").GetDouble(),
            Orbit = OrbitDTO.Parse(data.GetProperty("orbit")),
            OrbitIndex = data.GetProperty("orbitIndex").GetInt32(),
            Pressure = data.GetProperty("pressure").GetDouble(),
            Radiation = data.GetProperty("radiation").GetDouble(),
            Radius = data.GetProperty("radius").GetDouble(),
            Resources = data.GetProperty("resources").EnumerateArray().Select(PlanetResourceDTO.Parse).ToArray(),
            Sunlight = data.GetProperty("sunlight").GetDouble(),
            Surface = data.GetProperty("surface").GetBoolean(),
            Temperature = data.GetProperty("temperature").GetDouble(),
            Plots = data.GetProperty("plots").GetInt32(),
            Fertility = data.GetProperty("fertility").GetDouble(),
            BuildOptions = jsonElement.GetProperty("buildOptions").GetProperty("options").EnumerateArray().Select(BuildOptionDTO.Parse).ToArray(),
            CountryId = jsonElement.GetProperty("country").GetProperty("id").GetString(),
            GovernorId = jsonElement.GetProperty("governor").GetProperty("id").GetString(),
            Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency")),
            LocalRules = PlanetLocalRulesDTO.Parse(jsonElement.GetProperty("localRules")),
            PopulationId = jsonElement.GetProperty("populationId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("populationId"),
            CogcId = jsonElement.GetProperty("cogcId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("cogcId")
        };
    }
    
    [NodeResolver]
    public static Task<PlanetDTO> Get(string id, [Service] PlanetByIdDataLoader planetByIdDataLoader)
        => planetByIdDataLoader.LoadAsync(id);
}