using System.Text.Json.Serialization;

namespace ProsperousUniverse.API.JsonModels;

public sealed record PresenceList([property: JsonPropertyName("users")] PresentUser[] Users);