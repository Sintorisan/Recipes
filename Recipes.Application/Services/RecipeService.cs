using Recipes.Application.Interfaces;
using Recipes.Domain.Entities;
using Recipes.Infrastructure.Interfaces;
using System.Text.Json;

namespace Recipes.Application.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ISemanticKernelService _semanticKernelService;
    private readonly IHtmlService _htmlService;

    public RecipeService(IRecipeRepository recipeRepository, ISemanticKernelService semanticKernelService, IHtmlService htmlService)
    {
        _recipeRepository = recipeRepository;
        _semanticKernelService = semanticKernelService;
        _htmlService = htmlService;
    }

    public async Task<Recipe?> CreateAndAddRecipe(string url)
    {
        var html = await _htmlService.GetHtmlFromUrl(url);
        var result = await _semanticKernelService.CreateRecipeJson(html);
        var recipe = await MapRecipe(result);

        if (recipe == null)
        {
            return null;
        }

        var existingRecipe = await _recipeRepository.GetRecipeByNameAsync(recipe.Name);
        if (existingRecipe != null)
        {
            return existingRecipe;
        }

        await _recipeRepository.AddRecipeAsync(recipe);
        return recipe;
    }

    private async Task<Recipe?> MapRecipe(string recipe)
    {
        try
        {
            var mappedRecipe = JsonSerializer.Deserialize<Recipe>(recipe, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (mappedRecipe == null)
            {
                return null;
            }

            await AddMissingIngredientsToDb(mappedRecipe.Steps);

            return mappedRecipe;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing recipe: {ex.Message}");
            return null;
        }
    }

    private async Task AddMissingIngredientsToDb(List<Step> steps)
    {
        foreach (var step in steps)
        {
            foreach (var ingredient in step.Ingredients)
            {
                if (!await _recipeRepository.DoesIngredientExistByName(ingredient.Name))
                {
                    await _recipeRepository.CreateNewIngredient(ingredient);
                }
            }
        }
    }

    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        var recipes = await _recipeRepository.GetAllRecipesAsync();

        return recipes ?? new();
    }

    public async Task<Recipe?> GetRecipeAsync(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return await _recipeRepository.GetRecipeAsync(guid);
        }
        return null;
    }
}