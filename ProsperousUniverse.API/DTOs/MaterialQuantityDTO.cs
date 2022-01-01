using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("MaterialQuantity")]
[Authorize(Policy = "read:pu_material_quantity")]
public sealed class MaterialQuantityDTO
{
    [GraphQLIgnore]
    public string MaterialId { get; set; }

    [GraphQLName("material"), GraphQLNonNullType]
    public Task<MaterialDTO> GetMaterialAsync([Service] MaterialByIdDataLoader materialByIdDataLoader)
        => materialByIdDataLoader.LoadAsync(MaterialId);
    
    public int Amount { get; set; }

    public static MaterialQuantityDTO Parse(JsonElement jsonElement)
    {
        return new MaterialQuantityDTO()
        {
            MaterialId = jsonElement.GetProperty("material").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("material.id"),
            Amount = jsonElement.GetProperty("amount").GetInt32()
        };
    }
}