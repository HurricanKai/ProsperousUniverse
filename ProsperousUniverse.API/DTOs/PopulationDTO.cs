using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Population"), Node]
[Authorize(Policy = "read:pu_population")]
public sealed class PopulationDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }

    public static PopulationDTO Parse(JsonElement jsonElement)
    {
        throw new NotImplementedException();
    }

    [NodeResolver]
    public static Task<PopulationDTO> Get(string id, [Service] PopulationByIdDataLoader populationByIdDataLoader)
        => populationByIdDataLoader.LoadAsync(id);
}
