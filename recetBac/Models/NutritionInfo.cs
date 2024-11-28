using System.ComponentModel.DataAnnotations;

namespace recetBac.Models
{
    public class NutritionInfo
    {
        [Key]  // Marca NutritionId como la clave primaria
        public int NutritionId { get; set; }
        public string MealName { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public float Fiber { get; set; }
        public float Sugar { get; set; }
    }
}
