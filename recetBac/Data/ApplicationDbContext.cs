using Microsoft.EntityFrameworkCore;
using recetBac.Models;
using System.Collections.Generic;

namespace recetBac.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<NutritionInfo> NutritionInfos { get; set; }
    }
}
