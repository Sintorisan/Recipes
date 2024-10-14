namespace Recipes.Application.Interfaces;

public interface ISemanticKernelService
{
    Task<string> CreateRecipeJson(string urlScrape);
}