using Meals_API.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Meals_API.Services
{
    public class MealRetriever
    {
        public string URL { get; set; } = "https://www.themealdb.com/api/json/v1/1/";
        private static readonly string _randomMealResource = "random.php";
        private static readonly string _searchMealResource = "search.php?s=";
        private readonly RestClient _client;

        public MealRetriever()
        {
            _client = new RestClient(URL);
        }

        public async Task<Meal?> RetrieveRandomMeal()
        {
            try
            {
                var mealResponse = await ApiHelper.ExecuteRequestAsync<Meal>(_client, _randomMealResource);
                return mealResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
                return null;
            }
        }

        public async Task<Meal?> RetrieveMealByWord(string word)
        {
            try
            {
                var mealResponse = await ApiHelper.ExecuteRequestAsync<Meal>(_client, _searchMealResource + word);
                return mealResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
                return null;
            }
        }
    }
}
