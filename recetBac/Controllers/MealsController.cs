using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly MealService _mealService;

    public MealsController(MealService mealService)
    {
        _mealService = mealService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMealByName([FromQuery] string name)
    {
        var result = await _mealService.SearchMealByName(name);
        return Ok(result);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetMealDetails(string id)
    {
        var result = await _mealService.GetMealDetails(id);
        return Ok(result);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomMeal()
    {
        var result = await _mealService.GetRandomMeal();
        return Ok(result);
    }

    [HttpGet("categories-areas-ingredients")]
    public async Task<IActionResult> GetCategoriesAreasIngredients()
    {
        var result = await _mealService.GetCategoriesAreasIngredients();
        return Ok(result);
    }

    [HttpGet("filter/category")]
    public async Task<IActionResult> FilterByCategory([FromQuery] string category)
    {
        var result = await _mealService.FilterByCategory(category);
        return Ok(result);
    }

    [HttpGet("filter/area")]
    public async Task<IActionResult> FilterByArea([FromQuery] string area)
    {
        var result = await _mealService.FilterByArea(area);
        return Ok(result);
    }
}
