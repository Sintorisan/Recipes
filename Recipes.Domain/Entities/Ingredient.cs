using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Ingredient
{
    [JsonPropertyName("item")]
    public string Item { get; set; } = string.Empty;

    [JsonPropertyName("measurement")]
    public Measurement? Measurement { get; set; }
}