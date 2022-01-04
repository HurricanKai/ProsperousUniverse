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
    public async IAsyncEnumerable<IWorldEntityDTO> GetLinesAsync(
        [Service] WorldEntityResolver worldEntityResolver)
    {
        foreach (var (type, id) in Lines)
            yield return await worldEntityResolver.GetWorldEntity(type, id);
    }

    public static AddressDTO Parse(JsonElement jsonElement)
    {
        return new AddressDTO
        {
            Lines = jsonElement.GetProperty("lines").EnumerateArray().Select(x => (
                x.GetProperty("type").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("type"),
                x.GetProperty("entity").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("entity.id"))).ToArray()
        };
    }
}