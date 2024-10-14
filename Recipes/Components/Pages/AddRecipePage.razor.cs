using Microsoft.AspNetCore.Components;
using Recipes.Services;

namespace Recipes.Components.UIComponents;

public class AddRecipeLogic
{
    private readonly ApiService _apiService;
    private readonly NavigationManager _navigationManager;

    public AddRecipeLogic(ApiService apiService, NavigationManager navigationManager)
    {
        _apiService = apiService;
        _navigationManager = navigationManager;
    }

    public async Task<bool> CreateRecipe(string url)
    {
        var newRecipe = await _apiService.CreateRecipeAsync(url);

        if (newRecipe == null)
        {
            return false;
        }

        _navigationManager.NavigateTo($"/recipe/{newRecipe.Id}");

        return true;
    }
}