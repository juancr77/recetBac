using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recetBac.Data;
using recetBac.Models;

namespace recetBac.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NutritionInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NutritionInfo
        [HttpGet]
        public IActionResult GetAllNutritionInfo()
        {
            var nutritionInfos = _context.NutritionInfos.ToList();
            return Ok(nutritionInfos);
        }

        // GET: api/NutritionInfo/{id}
        [HttpGet("{id}")]
        public IActionResult GetNutritionInfoById(int id)
        {
            var nutritionInfo = _context.NutritionInfos.Find(id);
            if (nutritionInfo == null)
            {
                return NotFound(new { Message = "Nutrition info not found." });
            }

            return Ok(nutritionInfo);
        }

        // POST: api/NutritionInfo
        [HttpPost]
        public IActionResult CreateNutritionInfo([FromBody] NutritionInfo info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NutritionInfos.Add(info);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetNutritionInfoById), new { id = info.NutritionId }, info);
        }

        // PUT: api/NutritionInfo/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateNutritionInfo(int id, [FromBody] NutritionInfo updatedInfo)
        {
            if (id != updatedInfo.NutritionId)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingInfo = _context.NutritionInfos.Find(id);
            if (existingInfo == null)
            {
                return NotFound(new { Message = "Nutrition info not found." });
            }

            // Actualiza las propiedades específicas
            existingInfo.MealName = updatedInfo.MealName;
            existingInfo.Calories = updatedInfo.Calories;
            existingInfo.Protein = updatedInfo.Protein;
            existingInfo.Fat = updatedInfo.Fat;
            existingInfo.Carbohydrates = updatedInfo.Carbohydrates;
            existingInfo.Fiber = updatedInfo.Fiber;
            existingInfo.Sugar = updatedInfo.Sugar;

            _context.Entry(existingInfo).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/NutritionInfo/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNutritionInfo(int id)
        {
            var nutritionInfo = _context.NutritionInfos.Find(id);
            if (nutritionInfo == null)
            {
                return NotFound(new { Message = "Nutrition info not found." });
            }

            _context.NutritionInfos.Remove(nutritionInfo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
