using Recipes.Domain.Entities;
using System.Net.Http.Json;

namespace Recipes.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Recipe?> AddRecipeAsync(string url)
        {
            try
            {
                // Send the POST request
                var response = await _httpClient.PostAsJsonAsync("api/recipe", url);
                response.EnsureSuccessStatusCode();

                // Deserialize and return the recipe
                return await response.Content.ReadFromJsonAsync<Recipe>();
            }
            catch (HttpRequestException ex)
            {
                // Handle the HTTP request error (e.g., logging)
                Console.WriteLine($"Error fetching recipe: {ex.Message}");
                return null;
            }
        }
    }
}