using System.Text.Json.Serialization;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;
using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.JsonModels;

[GraphQLName("PresentUser")]
[Authorize(Policy = "read:pu_present_user")]
public sealed record PresentUser(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("time")] long Time)
{
    [GraphQLName("user")]
    public Task<UserDTO> GetUserAsync([Service] UserByIdDataLoader userByIdDataLoader)
        => userByIdDataLoader.LoadAsync(Id);
}
