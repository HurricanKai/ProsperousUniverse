using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Material"), Node]
[Authorize(Policy = "read:material")]
public sealed class MaterialDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Ticker { get; set; }
    
    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLIgnore]
    public string CategoryId { get; set; }

    [GraphQLName("category")]
    public Task<MaterialCategoryDTO> GetMaterialCategoryAsync(
        [Service] MaterialCategoryByIdDataLoader materialCategoryByIdDataLoader)
        => materialCategoryByIdDataLoader.LoadAsync(CategoryId);

    public double Weight { get; set; }

    public double Volume { get; set; }

    public bool IsResource { get; set; }

    public string? ResourceType { get; set; } // TODO: Can we resolve this to something more useful?

    public bool CogcUsage { get; set; }
    
    public bool WorkforceUsage { get; set; }

    public RecipeDTO[] InputRecipes { get; set; }
    public RecipeDTO[] OutputRecipes { get; set; }
    public BuildingRecipeDTO[] BuildingRecipes { get; set; }
    public MaterialInfrastructureUsageDTO[] InfrastructureUsages { get; set; }

    public static MaterialDTO Parse(JsonElement jsonElement)
    {
        var matInfo = jsonElement.GetProperty("material");
        return new MaterialDTO
        {
            Id = matInfo.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Ticker = matInfo.GetProperty("ticker").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("ticker"),
            Name = matInfo.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            CategoryId = matInfo.GetProperty("category").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("category"),
            Weight = matInfo.GetProperty("weight").GetDouble(),
            Volume = matInfo.GetProperty("volume").GetDouble(),
            IsResource = jsonElement.GetProperty("isResource").GetBoolean(),
            ResourceType = jsonElement.GetProperty("resourceType").GetString(),
            CogcUsage = jsonElement.GetProperty("cogcUsage").GetBoolean(),
            WorkforceUsage = jsonElement.GetProperty("workforceUsage").GetBoolean(),
            InputRecipes = jsonElement.GetProperty("inputRecipes").EnumerateArray().Select(RecipeDTO.Parse).ToArray(),
            OutputRecipes = jsonElement.GetProperty("outputRecipes").EnumerateArray().Select(RecipeDTO.Parse).ToArray(),
            BuildingRecipes = jsonElement.GetProperty("buildingRecipes").EnumerateArray().Select(BuildingRecipeDTO.Parse).ToArray(),
            InfrastructureUsages = jsonElement.GetProperty("infrastructureUsage").EnumerateArray().Select(MaterialInfrastructureUsageDTO.Parse).ToArray()
        };
    }

    [NodeResolver]
    public static Task<MaterialDTO> Get(string ticker, [Service] MaterialByIdDataLoader materialByIdDataLoader)
        => materialByIdDataLoader.LoadAsync(ticker);
}