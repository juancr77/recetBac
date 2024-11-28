using Microsoft.AspNetCore.Mvc;
using RecetasAPI.Services;
using System.Net.Http;

namespace recetBac.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionixController : ControllerBase
    {
        private readonly NutritionixService _nutritionixService;

        public NutritionixController(NutritionixService nutritionixService)
        {
            _nutritionixService = nutritionixService;
        }

        // 1. Buscar alimentos por nombre
        [HttpGet("search")]
        public async Task<IActionResult> SearchFood(string query)
        {
            var result = await _nutritionixService.SearchFoodAsync(query);
            if (result == null)
                return NotFound("No results found.");
            return Ok(result);
        }

        // 2. Obtener información detallada de un alimento
        [HttpGet("nutrients")]
        public async Task<IActionResult> GetFoodDetails(string query)
        {
            var result = await _nutritionixService.GetFoodDetailsAsync(query);
            if (result == null)
                return BadRequest("Unable to process request.");
            return Ok(result);
        }

        // 3. Buscar alimentos por código UPC
        [HttpGet("item")]
        public async Task<IActionResult> SearchByUPC(string upc)
        {
            var result = await _nutritionixService.SearchByUPCAsync(upc);
            if (result == null)
                return NotFound("No results found.");
            return Ok(result);
        }
    }
}
