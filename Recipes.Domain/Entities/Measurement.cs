using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Measurement
{
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
}