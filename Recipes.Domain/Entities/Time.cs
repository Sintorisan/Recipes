using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Time
{
    [JsonPropertyName("prep")]
    public string Prep { get; set; } = string.Empty;

    [JsonPropertyName("cook")]
    public string Cook { get; set; } = string.Empty;

    [JsonPropertyName("total")]
    public string Total { get; set; } = string.Empty;
}