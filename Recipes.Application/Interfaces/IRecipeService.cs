using Recipes.Domain.Entities;

namespace Recipes.Application.Interfaces;

public interface IRecipeService
{
    Task<Recipe?> GetRecipeAsync(string id);

    Task<List<Recipe>> GetAllRecipesAsync();

    Task<Recipe?> CreateAndAddRecipe(string url);
}