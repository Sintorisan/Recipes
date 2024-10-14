using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Step
{
    public Guid Id { get; set; }

    [JsonPropertyName("how_to")]
    public string HowTo { get; set; } = string.Empty;

    [JsonPropertyName("ingredients")]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}