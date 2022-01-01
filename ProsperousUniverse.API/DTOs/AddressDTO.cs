using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Address")]
[Authorize(Policy = "read:pu_address")]
public sealed class AddressDTO
{
    [GraphQLIgnore]
    public (string Type, string Id)[] Lines { get; set; }

    [GraphQLName("lines")]
    public async Task<List<IWorldEntityDTO>> GetLinesAsync(
        [Service] PlanetByIdDataLoader planetByIdDataLoader,
        [Service] SystemByIdDataLoader systemByIdDataLoader)
    {
        var list = new List<IWorldEntityDTO>();
        foreach (var (type, id) in Lines)
        {
            list.Add(type switch
            {
                "PLANET" => await planetByIdDataLoader.LoadAsync(id),
                "SYSTEM" => await systemByIdDataLoader.LoadAsync(id),
                _ => FallbackEntity(type)
            });
        }

        return list;
    }

    private static IWorldEntityDTO FallbackEntity(string type)
    {
        throw new InvalidOperationException($"Unknown Type \"{type}\"");
    }

    public static AddressDTO Parse(JsonElement jsonElement)
    {
        return new AddressDTO
        {
            Lines = jsonElement.GetProperty("lines").EnumerateArray().Select(x => (
                x.GetProperty("type").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("type"),
                x.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"))).ToArray()
        };
    }
}