using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Interfaces;
using Recipes.Domain.Entities;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<ActionResult<Recipe?>> AddRecipe([FromBody] string url)
        {
            var recipe = await _recipeService.CreateAndAddRecipe(url);

            if (recipe == null)
            {
                return BadRequest("Recipe could not be created.");
            }

            return Ok(recipe);
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
        {
            var request = await _recipeService.GetAllRecipesAsync();

            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(string id)
        {
            var request = await _recipeService.GetRecipeAsync(id);

            return Ok(request);
        }
    }
}