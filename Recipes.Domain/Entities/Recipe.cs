using System.Text.Json.Serialization;

namespace Recipes.Domain.Entities;

public class Recipe
{
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("img_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("servings")]
    public int Servings { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new List<string>();

    [JsonPropertyName("meal_tag")]
    public string MealTag { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("total_time")]
    public int TotalTime { get; set; }

    [JsonPropertyName("steps")]
    public List<Step> Steps { get; set; } = new List<Step>();

    [JsonIgnore]
    public List<Ingredient> AllIngredients
    {
        get { return Steps.SelectMany(s => s.Ingredients).ToList(); }
    }
}