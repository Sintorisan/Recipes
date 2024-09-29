using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Step
{
    [JsonPropertyName("how_to")]
    public string HowTo { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("ingredients")]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}