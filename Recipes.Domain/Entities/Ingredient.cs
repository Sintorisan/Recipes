using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    public bool IsInCart { get; set; }

    [JsonPropertyName("measurement")]
    public Measurement? Measurement { get; set; }

    [JsonIgnore]
    public List<Recipe>? Recipes { get; set; }

    [JsonIgnore]
    public List<Step>? Steps { get; set; }
}