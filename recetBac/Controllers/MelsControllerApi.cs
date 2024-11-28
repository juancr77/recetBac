using Microsoft.AspNetCore.Mvc;
using recetBac.Data;
using recetBac.Models;

namespace recetBac.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MelsControllerApi : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MelsControllerApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mels
        [HttpGet]
        public IActionResult GetAllMeals()
        {
            return Ok(_context.Meals.ToList());
        }

        // GET: api/Mels/{id}
        [HttpGet("{id}")]
        public IActionResult GetMealById(int id)
        {
            var meal = _context.Meals.FirstOrDefault(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found" });
            }
            return Ok(meal);
        }

        // POST: api/Mels
        [HttpPost]
        public IActionResult CreateMeal([FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Meals.Add(meal);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMealById), new { id = meal.MealId }, meal);
        }

        // PUT: api/Mels/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMeal(int id, [FromBody] Meal meal)
        {
            if (id != meal.MealId)
            {
                return BadRequest(new { message = "Meal ID mismatch" });
            }

            var existingMeal = _context.Meals.FirstOrDefault(m => m.MealId == id);
            if (existingMeal == null)
            {
                return NotFound(new { message = "Meal not found" });
            }

            // Update properties
            existingMeal.Name = meal.Name;
            existingMeal.Category = meal.Category;
            existingMeal.Area = meal.Area;
            existingMeal.Ingredients = meal.Ingredients;
            existingMeal.PreparationTime = meal.PreparationTime;
            existingMeal.CookingTime = meal.CookingTime;
            existingMeal.Instructions = meal.Instructions;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Mels/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMeal(int id)
        {
            var meal = _context.Meals.FirstOrDefault(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found" });
            }

            _context.Meals.Remove(meal);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
