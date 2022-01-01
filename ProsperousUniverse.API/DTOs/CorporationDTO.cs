using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("Corporation")]
[Authorize(Policy = "read:pu_corporation")]
public sealed class CorporationDTO : INode
{
    [GraphQLType(typeof(IdType)), GraphQLNonNullType]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string Code { get; set; }
    
    public DateTime Founded { get; set; }
    
    [GraphQLIgnore]
    public string CountryId { get; set; }

    [GraphQLNonNullType, GraphQLName("country")]
    public Task<CountryDTO> GetCountryAsync([Service] CountryByIdDataLoader countryByIdDataLoader)
        => countryByIdDataLoader.LoadAsync(CountryId);

    public int TotalShares { get; set; }
    
    public bool HeadquartersFinished { get; set; }
    public AddressDTO HeadquartersAddress { get; set; }

    [GraphQLNonNullType]
    public CorporationShareholderDTO[] Shareholders { get; set; }

    public static CorporationDTO Parse(JsonElement jsonElement)
    {
        return new CorporationDTO
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Code = jsonElement.GetProperty("code").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("code"),
            Founded = DateTime.UnixEpoch + TimeSpan.FromMilliseconds(jsonElement.GetProperty("founded").GetProperty("timestamp").GetInt64()),
            CountryId = jsonElement.GetProperty("country").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("country.id"),
            TotalShares = jsonElement.GetProperty("totalShares").GetInt32(),
            Shareholders = jsonElement.GetProperty("shareholders").EnumerateArray().Select(CorporationShareholderDTO.Parse).ToArray()
        };
    }

    [NodeResolver]
    public static Task<CorporationDTO> Get(
        string id,
        [Service] CorporationByIdDataLoader corporationByIdDataLoader)
        => corporationByIdDataLoader.LoadAsync(id);
}