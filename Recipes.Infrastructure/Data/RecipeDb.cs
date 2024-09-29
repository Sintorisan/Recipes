using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;

namespace Recipes.Infrastructure.Data;

public class RecipeDb : DbContext
{
    public RecipeDb(DbContextOptions<RecipeDb> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
}