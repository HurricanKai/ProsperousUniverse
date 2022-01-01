using System.Text.Json;

namespace ProsperousUniverse.API;

public static class ParsingUtils
{
    internal static T ThrowRequiredPropertyMissing<T>(string name)
        => throw new InvalidOperationException($"Required property missing: {name}");
    
    internal static string ThrowRequiredPropertyMissing(string name)
        => ThrowRequiredPropertyMissing<string>(name);

    internal static DateTime ParseUnixWrappedTimestamp(JsonElement jsonElement) 
        => DateTime.UnixEpoch + TimeSpan.FromMilliseconds(jsonElement.GetProperty("timestamp").GetInt64());
}
