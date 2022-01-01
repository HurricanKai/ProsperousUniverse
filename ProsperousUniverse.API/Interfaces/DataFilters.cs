using System.Text.Json.Serialization;

namespace ProsperousUniverse.API.Interfaces;

public sealed class DataFilters
{
    [JsonPropertyName("find")]
    public string? Find { get; set; }
}