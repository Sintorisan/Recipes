using Recipes.Domain.Entities;

namespace Recipes.Infrastructure.Interfaces;

public interface IRecipeRepository
{
    Task AddRecipeAsync(Recipe recipe);

    Task<List<Recipe>> GetAllRecipesAsync();

    Task<Recipe?> GetRecipeAsync(Guid id);

    Task<List<Ingredient>> GetIngredientsAsync();

    Task AddIngredientsAsync(Ingredient ingredient);

    Task CreateNewIngredient(Ingredient ingredient);

    Task<Recipe?> GetRecipeByNameAsync(string name);

    Task<bool> DoesIngredientExistByName(string name);
}