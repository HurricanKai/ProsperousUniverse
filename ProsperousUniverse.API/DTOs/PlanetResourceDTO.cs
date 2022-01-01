using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("PlanetResource")]
[Authorize(Policy = "read:pu_planet_resource")]
public sealed class PlanetResourceDTO
{
    public double Factor { get; set; }

    public string Type { get; set; }

    [GraphQLIgnore] public string MaterialId { get; set; }

    [GraphQLName("material")]
    public Task<MaterialDTO> GetMaterialAsync([Service] MaterialByIdDataLoader materialByIdDataLoader)
        => materialByIdDataLoader.LoadAsync(MaterialId);

    public static PlanetResourceDTO Parse(JsonElement jsonElement) 
        => new()
        {
            MaterialId = jsonElement.GetProperty("materialId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("materialId"),
            Factor = jsonElement.GetProperty("factor").GetDouble(),
            Type = jsonElement.GetProperty("type").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("type")
        };
}