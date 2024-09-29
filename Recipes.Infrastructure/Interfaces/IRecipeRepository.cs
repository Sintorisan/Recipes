using Recipes.Domain.Entities;

namespace Recipes.Infrastructure.Interfaces;

public interface IRecipeRepository
{
    Task AddToDatabaseAsync(Recipe recipe);
}