using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Infrastructure.Data;
using Recipes.Infrastructure.Interfaces;

namespace Recipes.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeDb _context;

    public RecipeRepository(RecipeDb context)
    {
        _context = context;
    }

    public async Task AddRecipeAsync(Recipe recipe)
    {
        recipe.Id = Guid.NewGuid();

        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task<Recipe?> GetRecipeByNameAsync(string name)
    {
        return await _context.Recipes
            .Include(r => r.AllIngredients)
                .ThenInclude(i => i.Measurement)
            .Include(r => r.Steps)
                .ThenInclude(s => s.Ingredients)
                    .ThenInclude(i => i.Measurement)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipe?> GetRecipeAsync(Guid id)
    {
        return await _context.Recipes
           .Include(r => r.AllIngredients)
               .ThenInclude(i => i.Measurement)
           .Include(r => r.Steps)
               .ThenInclude(s => s.Ingredients)
                   .ThenInclude(i => i.Measurement)
           .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Ingredient>> GetIngredientsAsync() => await _context.Ingredients.ToListAsync();

    public async Task<bool> DoesIngredientExistByName(string name)
    {
        return await _context.Ingredients.AnyAsync(i => i.Name == name);
    }

    public async Task CreateNewIngredient(Ingredient ingredient)
    {
        var newIngredient = new Ingredient()
        {
            Id = Guid.NewGuid(),
            Name = ingredient.Name,
            Measurement = null
        };

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }
}