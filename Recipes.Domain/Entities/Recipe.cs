using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Recipe
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public Time? Time { get; set; }

    [JsonPropertyName("steps")]
    public List<Step> Steps { get; set; } = new List<Step>();
}