using Meals_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Meals_API.Services
{
    public class MealService
    {
        private readonly AppDbContext _dbContext;

        public MealService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<MealSearch?> Create(MealSearch search)
        {
            _dbContext.Searches.Add(search);
            await _dbContext.SaveChangesAsync();
            return search;
        }

        public async Task<List<MealSearch>> GetAll()
        {
            return await _dbContext.Searches.OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        public async Task<MealSearch?> GetById(int id)
        {
            return await _dbContext.Searches.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<MealSearch?> GetByQuery(string query)
        {
            return await _dbContext.Searches.FirstOrDefaultAsync(s => s.SearchQuery == query);
        }

        public async Task<bool> DeleteById(int id)
        {
            var search = await _dbContext.Searches.FindAsync(id);
            if (search == null)
            {
                return false; // Record non trovato
            }

            _dbContext.Searches.Remove(search);
            await _dbContext.SaveChangesAsync();
            return true; // Record eliminato con successo
        }
    }
}
