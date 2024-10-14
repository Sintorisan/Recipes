using HtmlAgilityPack;
using Recipes.Application.Interfaces;

namespace Recipes.Application.Services;

public class HtmlService : IHtmlService
{
    private readonly HttpClient _httpClient;

    public HtmlService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetHtmlFromUrl(string url)
    {
        var response = await _httpClient.GetStringAsync(url);

        string pageContent = ExtractBodyContent(response);

        return pageContent;
    }

    private static string ExtractBodyContent(string html)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        var tagsToRemove = new[] { "script", "style", "link", "meta", "header", "footer", "nav", "aside", "iframe", "embed", "form", "button", "input" };

        foreach (var tag in tagsToRemove)
        {
            foreach (var node in doc.DocumentNode.SelectNodes("//" + tag) ?? new HtmlNodeCollection(null))
            {
                node.Remove();
            }
        }

        var bodyContent = doc.DocumentNode.SelectSingleNode("//body").InnerHtml;
        return bodyContent.Trim();
    }
}