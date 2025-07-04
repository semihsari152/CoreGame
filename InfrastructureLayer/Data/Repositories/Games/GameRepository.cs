using DomainLayer.Entities.Games;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.Data.Context;
using InfrastructureLayer.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Repositories.Games
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(CoreGameDbContext context) : base(context)
        {
        }

        public async Task<List<Game>> GetGamesByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == categoryId))
                .OrderBy(g => g.Title)
                .ToListAsync();
        }

        public async Task<List<Game>> GetGamesByPlatformAsync(int platformId)
        {
            return await _dbSet
                .Include(g => g.GamePlatforms)
                .ThenInclude(gp => gp.Platform)
                .Where(g => g.GamePlatforms.Any(gp => gp.PlatformId == platformId && gp.IsAvailable))
                .OrderBy(g => g.Title)
                .ToListAsync();
        }

        public async Task<List<Game>> SearchGamesAsync(string searchTerm)
        {
            var term = searchTerm.ToLower().Trim();

            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Where(g =>
                    g.Title.ToLower().Contains(term) ||
                    g.Description.ToLower().Contains(term) ||
                    g.Developer.ToLower().Contains(term) ||
                    g.Publisher.ToLower().Contains(term) ||
                    (g.Tags != null && g.Tags.ToLower().Contains(term)))
                .OrderByDescending(g => g.AverageRating)
                .ThenBy(g => g.Title)
                .ToListAsync();
        }

        public async Task<List<Game>> GetFeaturedGamesAsync(int count = 10)
        {
            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Where(g => g.Status == DomainLayer.Enums.GameStatus.Published)
                .OrderByDescending(g => g.AverageRating)
                .ThenByDescending(g => g.ViewCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Game>> GetRecentlyAddedGamesAsync(int count = 10)
        {
            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Where(g => g.Status == DomainLayer.Enums.GameStatus.Published)
                .OrderByDescending(g => g.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Game>> GetTopRatedGamesAsync(int count = 10)
        {
            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Where(g => g.Status == DomainLayer.Enums.GameStatus.Published && g.TotalRatings >= 5)
                .OrderByDescending(g => g.AverageRating)
                .ThenByDescending(g => g.TotalRatings)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Game>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
                .Where(g => g.Price >= minPrice && g.Price <= maxPrice && g.Status == DomainLayer.Enums.GameStatus.Published)
                .OrderBy(g => g.Price)
                .ToListAsync();
        }

        public async Task<List<Game>> GetDiscountedGamesAsync()
        {
            return await _dbSet
                .Where(g => g.DiscountPrice.HasValue && g.DiscountPrice < g.Price && g.Status == DomainLayer.Enums.GameStatus.Published)
                .OrderByDescending(g => g.DiscountPercentage)
                .ToListAsync();
        }

        public async Task<Game?> GetGameWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Include(g => g.GamePlatforms)
                .ThenInclude(gp => gp.Platform)
                .Include(g => g.GameTags)
                .ThenInclude(gt => gt.Tag)
                .Include(g => g.GameImages)
                .Include(g => g.GameReviews.Take(5)) // Son 5 review
                .ThenInclude(gr => gr.User)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game?> GetGameBySlugAsync(string slug)
        {
            return await GetGameWithDetailsAsync(0); // Slug implementasyonu eklenecek
        }

        public async Task UpdateGameRatingAsync(int gameId, decimal newRating, int ratingCount)
        {
            var game = await GetByIdAsync(gameId);
            if (game != null)
            {
                game.AverageRating = newRating;
                game.TotalRatings = ratingCount;
                Update(game);
            }
        }

        public async Task<List<Game>> GetSimilarGamesAsync(int gameId, int count = 5)
        {
            var game = await _dbSet
                .Include(g => g.GameCategories)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return new List<Game>();

            var categoryIds = game.GameCategories.Select(gc => gc.CategoryId).ToList();

            return await _dbSet
                .Include(g => g.GameCategories)
                .Where(g => g.Id != gameId &&
                           g.GameCategories.Any(gc => categoryIds.Contains(gc.CategoryId)) &&
                           g.Status == DomainLayer.Enums.GameStatus.Published)
                .OrderByDescending(g => g.AverageRating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetGameViewCountsAsync(List<int> gameIds)
        {
            return await _dbSet
                .Where(g => gameIds.Contains(g.Id))
                .ToDictionaryAsync(g => g.Id, g => g.ViewCount);
        }
    }
}
