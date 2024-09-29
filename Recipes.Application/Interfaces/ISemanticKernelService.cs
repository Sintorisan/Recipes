using Recipes.Domain.Entities;

namespace Recipes.Application.Interfaces;

public interface ISemanticKernelService
{
    Task<Recipe> CreateRecipe(string urlScrape);
}