using Meals_API.Models;
using Meals_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meals_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private MealRetriever? _mealRetriever;
        private static int _idChiamata = 0;

        [HttpGet("random")]
        public async Task<ActionResult> GetRandomMeal([FromServices] MealService mealService)
        {
            try
            {
                // Esegui la richiesta per un piatto casuale
                _mealRetriever = new MealRetriever();
                var meal = await _mealRetriever.RetrieveRandomMeal() ?? throw new Exception($"Risposta inesistente");

                // Estrai il nome del piatto
                string mealName = NormalizeText(meal.Meals.First().strMeal);

                // Controlla se esiste già nel database
                var existingSearch = await mealService.GetByQuery(mealName);

                if (existingSearch == null)
                {
                    // Salva la ricerca come successo
                    var successfulSearch = new MealSearch
                    {
                        SearchQuery = mealName,
                        CreatedAt = DateTime.Now,
                        SearchSuccess = true
                    };
                    await mealService.Create(successfulSearch);
                }

                _idChiamata++;
                Console.WriteLine("\nChiamata API " + _idChiamata);
                Console.WriteLine(meal);

                return Ok(meal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSearches([FromServices] MealService mealService)
        {
            try
            {
                var result = await mealService.GetAll();

                if (result.Count == 0)
                {
                    return NotFound(new { error = $"Lista ricerche vuota" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }

        [HttpPost("search")]
        public async Task<ActionResult> SaveSearchAndFetchMeal([FromServices] MealService mealService, [FromQuery] string s)
        {
            try
            {
                // Normalizza la stringa di ricerca
                string normalizedSearch = NormalizeText(s);

                // Controlla se la ricerca esiste già nel database
                var existingSearch = await mealService.GetByQuery(normalizedSearch);

                // Esegui la ricerca con l'API esterna
                _mealRetriever = new MealRetriever();
                var meal = await _mealRetriever.RetrieveMealByWord(s);

                // Controlla se la ricerca ha restituito risultati
                if (meal?.Meals == null || !meal.Meals.Any())
                {
                    if (existingSearch == null)
                    {
                        // Salva la ricerca fallita solo se non esiste già
                        var search = new MealSearch
                        {
                            SearchQuery = normalizedSearch,
                            CreatedAt = DateTime.Now,
                            SearchSuccess = false
                        };
                        await mealService.Create(search);
                    }
                    return NotFound(new { error = $"Nessun piatto trovato con il nome '{s}'" });
                }

                // Salva la ricerca riuscita solo se non esiste già
                if (existingSearch == null)
                {
                    var search = new MealSearch
                    {
                        SearchQuery = normalizedSearch,
                        CreatedAt = DateTime.Now,
                        SearchSuccess = true
                    };
                    await mealService.Create(search);
                }

                return Ok(meal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteSearchById([FromServices] MealService mealService, int id)
        {
            try
            {
                var result = await mealService.DeleteById(id);
                if (!result)
                {
                    return NotFound(new { error = $"Ricerca con ID '{id}' non trovata." });
                }

                return Ok(new { message = $"Ricerca con ID '{id}' eliminata con successo." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }

        private static string NormalizeText(string input)
        {
            return input.Trim().ToUpper();
        }
    }
}
