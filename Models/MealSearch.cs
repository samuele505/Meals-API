using System.ComponentModel.DataAnnotations;

namespace Meals_API.Models
{
    public class MealSearch
    {
        [Key]
        public int Id { get; set; }
        public string? SearchQuery { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool SearchSuccess { get; set; }
    }
}
