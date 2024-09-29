using Recipes.Application.Interfaces;
using System.Text.RegularExpressions;

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
        var jsonPattern = @"<body[^>]*>(.*?)<\/body>";
        var match = Regex.Match(html, jsonPattern, RegexOptions.Singleline);

        if (match.Success)
        {
            var jsonContent = match.Groups[1].Value;

            return jsonContent.Trim();
        }
        else
        {
            throw new Exception("JSON content not found in the HTML response.");
        }
    }
}