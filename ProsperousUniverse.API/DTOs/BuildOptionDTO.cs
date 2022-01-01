using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("BuildOption")]
[Authorize(Policy = "read:pu_build_option")]
public sealed class BuildOptionDTO
{
    [GraphQLNonNullType]
    public string SiteType { get; set; }
    
    [GraphQLNonNullType]
    public MaterialQuantityDTO[] BillOfMaterial { get; set; }

    public static BuildOptionDTO Parse(JsonElement jsonElement)
    {
        return new BuildOptionDTO()
        {
            SiteType =
                jsonElement.GetProperty("siteType").GetString() ??
                ParsingUtils.ThrowRequiredPropertyMissing("siteType"),
            BillOfMaterial = jsonElement.GetProperty("billOfMaterial").GetProperty("quantities").EnumerateArray()
                .Select(MaterialQuantityDTO.Parse).ToArray()
        };
    }
}