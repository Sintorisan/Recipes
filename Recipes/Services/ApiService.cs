using Recipes.Domain.Entities;
using System.Net.Http.Json;

namespace Recipes.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Recipe?> CreateRecipeAsync(string url)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/recipe", url);
            response.EnsureSuccessStatusCode();

            var recipe = await response.Content.ReadFromJsonAsync<Recipe>();

            return recipe;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching recipe: {ex.Message}");
            return null;
        }
    }

    public async Task<Recipe?> GetRecipe(string id)
    {
        var recipe = await _httpClient.GetFromJsonAsync<Recipe>($"api/recipe/{id}");

        return recipe;
    }

    public async Task<List<Recipe>?> GetAllRecipes()
    {
        var recipes = await _httpClient.GetFromJsonAsync<List<Recipe>>("api/recipe/");

        return recipes;
    }
}