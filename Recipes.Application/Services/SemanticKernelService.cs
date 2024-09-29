using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

using Recipes.Application.Interfaces;
using Recipes.Domain.Entities;
using Recipes.Infrastructure.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RecipeStorage.Application.Services;

public class SemanticKernelService : ISemanticKernelService
{
    private readonly IRecipeRepository _recipeRepository;

    private readonly Kernel _kernel;
    private readonly ChatHistory _history = new ChatHistory();
    private readonly IChatCompletionService _chatCompletionService;

    private string InitialPrompt => File.ReadAllText(
        Path.Combine(Directory.GetCurrentDirectory(), "SemanticKernel", "InitialPrompt.txt"));

    private readonly OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    public SemanticKernelService(IConfiguration config, Kernel kernel, IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
        _kernel = kernel;

        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        _history.AddAssistantMessage(InitialPrompt);
    }

    public async Task<Recipe> CreateRecipe(string urlScrape)
    {
        _history.AddUserMessage(urlScrape);
        var result = await GetAnswer();

        var jsonRecipe = ConvertToJsonObject(result);

        var recipe = MapNewRecipe(jsonRecipe);

        await _recipeRepository.AddToDatabaseAsync(recipe);

        return recipe;
    }

    private async Task<string> GetAnswer()
    {
        var rawResponse = await _chatCompletionService
            .GetChatMessageContentAsync(_history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: _kernel);

        return rawResponse.ToString();
    }

    private string ConvertToJsonObject(string urlScrape)
    {
        var jsonPattern = @"```json\s*(\{.*?\})\s*```";
        var match = Regex.Match(urlScrape, jsonPattern, RegexOptions.Singleline);

        if (match.Success)
        {
            var jsonContent = match.Groups[1].Value;

            jsonContent = Regex.Replace(jsonContent, @"//.*", "");
            jsonContent = jsonContent.Trim();

            return jsonContent;
        }
        else
        {
            throw new Exception("JSON content not found in the response.");
        }
    }

    private Recipe MapNewRecipe(string recipe)
    {
        try
        {
            var mappedRecipe = JsonSerializer.Deserialize<Recipe>(recipe, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return mappedRecipe ?? new Recipe();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing recipe: {ex.Message}");
            return new Recipe();
        }
    }
}