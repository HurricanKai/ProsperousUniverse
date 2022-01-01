using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("BuildingRecipe")]
[Authorize(Policy = "read:pu_building_recipe")]
public sealed class BuildingRecipeDTO
{
    [GraphQLIgnore]
    public string ReactorId { get; set; }

    [GraphQLName("reactor"), GraphQLNonNullType]
    public Task<ReactorDTO> GetReactorAsync([Service] ReactorByIdDataLoader reactorByIdDataLoader)
        => reactorByIdDataLoader.LoadAsync(ReactorId);

    [GraphQLNonNullType]
    public MaterialQuantityDTO[] BuildingCosts { get; set; }

    public static BuildingRecipeDTO Parse(JsonElement jsonElement)
        => new()
        {
            ReactorId = jsonElement.GetProperty("reactorId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("reactorId"),
            BuildingCosts = jsonElement.GetProperty("buildingCosts").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray()
        };
}