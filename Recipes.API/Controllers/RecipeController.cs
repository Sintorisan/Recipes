using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Interfaces;
using Recipes.Domain.Entities;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ISemanticKernelService _semanticKernelService;
        private readonly IHtmlService _htmlService;

        public RecipeController(ISemanticKernelService semanticKernelService, IHtmlService htmlService)
        {
            _semanticKernelService = semanticKernelService;
            _htmlService = htmlService;
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] string url)
        {
            var html = await _htmlService.GetHtmlFromUrl(url);
            var result = await _semanticKernelService.CreateRecipe(html);

            return Ok(result);
        }
    }
}