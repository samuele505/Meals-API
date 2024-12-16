using Meals_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Meals_API
{
    public class AppDbContext : DbContext
    {
        public DbSet<MealSearch> Searches { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=meals.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealSearch>()
                .ToTable("Searches")
                .HasKey(s => s.Id);

            modelBuilder.Entity<MealSearch>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
