using System.Diagnostics;
using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("MaterialCategory")]
[Authorize(Policy = "read:pu_material_category")]
public sealed class MaterialCategoryDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLIgnore]
    public string[] MaterialIds { get; set; }

    [GraphQLNonNullType, GraphQLName("materials")]
    public async Task<List<MaterialDTO>> GetMaterialsAsync(
        [Service] MaterialByIdDataLoader materialByIdDataLoader)
    {
        var list = new List<MaterialDTO>(MaterialIds.Length);
        foreach(var id in MaterialIds)
            list.Add(await materialByIdDataLoader.LoadAsync(id));
        return list;
    }

    [NodeResolver]
    public static Task<MaterialCategoryDTO> Get(
        string id,
        [Service] MaterialCategoryByIdDataLoader materialCategoryByIdDataLoader)
        => materialCategoryByIdDataLoader.LoadAsync(id);

    public static MaterialCategoryDTO Parse(JsonElement jsonElement)
    {
        Debug.Assert(jsonElement.GetProperty("children").GetArrayLength() == 0);
        return new()
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            MaterialIds = jsonElement.GetProperty("materials").EnumerateArray().Select(x
                => x.GetProperty("id").GetString() ??
                   ParsingUtils.ThrowRequiredPropertyMissing("materials[?].ticker")).ToArray()
        };
    }
}
