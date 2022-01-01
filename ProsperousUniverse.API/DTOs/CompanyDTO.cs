using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("Company")]
[Authorize(Policy = "read:pu_company")]
public sealed class CompanyDTO : INode
{
    [GraphQLType(typeof(IdType)), GraphQLNonNullType]
    public string Id { get; set; }
    
    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string Code { get; set; }
    
    public DateTime Founded { get; set; }
    
    [GraphQLIgnore]
    public string UserId { get; set; }

    [GraphQLName("user")]
    public Task<UserDTO> GetUserAsync([Service] UserByIdDataLoader userByIdDataLoader)
    {
        return userByIdDataLoader.LoadAsync(UserId);
    }
    
    [GraphQLIgnore]
    public string CountryId { get; set; }

    [GraphQLName("country")]
    public Task<CountryDTO> GetCountryAsync([Service] CountryByIdDataLoader countryByIdDataLoader)
    {
        return countryByIdDataLoader.LoadAsync(CountryId);
    }
    
    [GraphQLIgnore]
    public string? CorporationId { get; set; }

    [GraphQLName("corporation")]
    public Task<CorporationDTO?> GetCorporationDTO([Service] CorporationByIdDataLoader corporationByIdDataLoader)
    {
        if (CorporationId is null) return Task.FromResult<CorporationDTO?>(null);
        return corporationByIdDataLoader.LoadAsync(CorporationId);
    }
    
    [GraphQLNonNullType]
    public RatingReportDTO RatingReport { get; set; }

    [NodeResolver]
    public static Task<CompanyDTO> Get(string id, [Service] CompanyByIdDataLoader companyByIdDataLoader)
    {
        return companyByIdDataLoader.LoadAsync(id);
    }

    public static CompanyDTO Parse(JsonElement jsonElement)
    {
        var id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id");
        var ratingReport = jsonElement.GetProperty("ratingReport");
        return new CompanyDTO
        {
            Id = id,
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Code = jsonElement.GetProperty("code").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("code"),
            Founded = ParsingUtils.ParseUnixWrappedTimestamp(jsonElement.GetProperty("founded")),
            UserId = jsonElement.GetProperty("user").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("user.id"),
            CountryId = jsonElement.GetProperty("country").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("country.id"),
            CorporationId = jsonElement.GetProperty("corporation").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("corporation.id"),
            RatingReport = RatingReportDTO.Parse(ratingReport)
        };
    }
}
