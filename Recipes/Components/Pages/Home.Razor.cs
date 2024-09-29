using Recipes.Domain.Entities;
using Recipes.Services;

namespace Recipes.Components.Pages
{
    public class HomeLogic
    {
        private readonly ApiService _apiService;

        public string UserInput { get; set; } = string.Empty;  // Property with default value

        public Recipe? ProcessedOutput { get; private set; }   // Nullable Recipe to handle initial null state

        public HomeLogic(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task ProcessInput()
        {
            if (!string.IsNullOrEmpty(UserInput))
            {
                // Fetch the processed output from the API using the input
                ProcessedOutput = await _apiService.AddRecipeAsync(UserInput);
            }
        }
    }
}