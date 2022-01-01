using System.Text.Json.Serialization;

namespace ProsperousUniverse.API.JsonModels;

public sealed record BaseMessage([property: JsonPropertyName("messageType")] string MessageType, [property: JsonPropertyName("payload")] object Payload, [property: JsonPropertyName("to")] string? To = null);