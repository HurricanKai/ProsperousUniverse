using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("MaterialInfrastructureUsage")]
[Authorize(Policy = "read:pu_material_infrastructure_usage")]
public sealed class MaterialInfrastructureUsageDTO
{
    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string Ticker { get; set; }
    
    [GraphQLNonNullType]
    public string ProjectIdentifier { get; set; }
    
    public static MaterialInfrastructureUsageDTO Parse(JsonElement jsonElement)
        => new()
        {
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Ticker = jsonElement.GetProperty("ticker").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("ticker"),
            ProjectIdentifier = jsonElement.GetProperty("projectIdentifier").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("projectIdentifier"),
        };
}