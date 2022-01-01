using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("WorkforceCapacity")]
[Authorize(Policy = "read:pu_workforce_capacity")]
public sealed class WorkforceCapacityDTO
{
    [GraphQLNonNullType]
    public string Level { get; set; }
    
    public int Capacity { get; set; }
    
    public static WorkforceCapacityDTO Parse(JsonElement jsonElement)
        => new()
        {
            Level = jsonElement.GetProperty("level").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("level"),
            Capacity = jsonElement.GetProperty("capacity").GetInt32()
        };
}