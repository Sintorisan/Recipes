using Recipes.Domain.Entities;
using Recipes.Services;

namespace Recipes.Components.UIComponents;

public class RecipeOverviewLogic
{
    private readonly ApiService _apiService;

    public RecipeOverviewLogic(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<Recipe>?> GetAllRecipes()
    {
        return await _apiService.GetAllRecipes();
    }
}