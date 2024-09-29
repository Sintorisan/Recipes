namespace Recipes.Application.Interfaces;

public interface IHtmlService
{
    Task<string> GetHtmlFromUrl(string url);
}