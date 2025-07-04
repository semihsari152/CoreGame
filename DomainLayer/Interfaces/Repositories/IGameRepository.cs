using DomainLayer.Entities.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        // Game-specific methods
        Task<List<Game>> GetGamesByCategoryAsync(int categoryId);
        Task<List<Game>> GetGamesByPlatformAsync(int platformId);
        Task<List<Game>> SearchGamesAsync(string searchTerm);
        Task<List<Game>> GetFeaturedGamesAsync(int count = 10);
        Task<List<Game>> GetRecentlyAddedGamesAsync(int count = 10);
        Task<List<Game>> GetTopRatedGamesAsync(int count = 10);
        Task<List<Game>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<Game>> GetDiscountedGamesAsync();
        Task<Game?> GetGameWithDetailsAsync(int id);
        Task<Game?> GetGameBySlugAsync(string slug);
        Task UpdateGameRatingAsync(int gameId, decimal newRating, int ratingCount);
        Task<List<Game>> GetSimilarGamesAsync(int gameId, int count = 5);
        Task<Dictionary<int, int>> GetGameViewCountsAsync(List<int> gameIds);
    }
}
