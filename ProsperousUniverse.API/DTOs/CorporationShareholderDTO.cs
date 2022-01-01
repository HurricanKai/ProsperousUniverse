using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("CorporationShareholder")]
[Authorize(Policy = "read:pu_corporation_shareholder")]
public sealed class CorporationShareholderDTO
{
    public int Shares { get; set; }
    public double RelativeShare { get; set; }
    
    [GraphQLIgnore]
    public string CompanyId { get; set; }

    [GraphQLNonNullType, GraphQLName("company")]
    public Task<CompanyDTO> GetCompanyAsync([Service] CompanyByIdDataLoader companyByIdDataLoader)
        => companyByIdDataLoader.LoadAsync(CompanyId);

    public static CorporationShareholderDTO Parse(JsonElement jsonElement)
    {
        return new CorporationShareholderDTO()
        {
            Shares = jsonElement.GetProperty("shares").GetInt32(),
            RelativeShare = jsonElement.GetProperty("relativeShare").GetDouble(),
            CompanyId = jsonElement.GetProperty("company").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("company.id")
        };
    }
}