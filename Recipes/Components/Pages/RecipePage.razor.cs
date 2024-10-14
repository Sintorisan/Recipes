using Recipes.Domain.Entities;
using Recipes.Services;

namespace Recipes.Components.Pages;

public class RecipePageLogic
{
    private readonly ApiService _apiService;

    public RecipePageLogic(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<Recipe?> GetRecipe(string id)
    {
        var recipe = await _apiService.GetRecipe(id);

        return recipe;
    }
}