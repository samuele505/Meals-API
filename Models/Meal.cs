using Newtonsoft.Json;
using System.Text;

namespace Meals_API.Models
{
    public class Meal
    {
        [JsonProperty("meals")]
        public List<MealInfo>? Meals { get; set; }

        public override string? ToString()
        {
            if (Meals == null || Meals.Count == 0)
            {
                return "Nessuna ricetta disponibile.";
            }

            var sb = new StringBuilder();
            foreach (var ricetta in Meals)
            {
                sb.AppendLine($"Nome: {ricetta.strMeal}");
                sb.AppendLine($"URL Immagine: {ricetta.strMealThumb}");
            }

            return sb.ToString();
        }
    }

    public class MealInfo
    {
        public string? strMeal { get; set; }
        public string? strCategory { get; set; }
        public string? strArea { get; set; }
        public string? strInstructions { get; set; }
        public string? strMealThumb { get; set; }
        public string? strYoutube { get; set; }
        public string? strIngredient1 { get; set; }
        public string? strIngredient2 { get; set; }
        public string? strIngredient3 { get; set; }
        public string? strIngredient4 { get; set; }
        public string? strIngredient5 { get; set; }
        public string? strIngredient6 { get; set; }
        public string? strIngredient7 { get; set; }
        public string? strIngredient8 { get; set; }
        public string? strIngredient9 { get; set; }
        public string? strIngredient10 { get; set; }
        public string? strIngredient11 { get; set; }
        public string? strIngredient12 { get; set; }
        public string? strIngredient13 { get; set; }
        public string? strIngredient14 { get; set; }
        public string? strIngredient15 { get; set; }
        public string? strIngredient16 { get; set; }
        public string? strIngredient17 { get; set; }
        public string? strIngredient18 { get; set; }
        public string? strIngredient19 { get; set; }
        public string? strIngredient20 { get; set; }
        public string? strSource { get; set; }
    }
}
