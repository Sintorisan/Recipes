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

    public async Task AddToDatabaseAsync(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
    }
}