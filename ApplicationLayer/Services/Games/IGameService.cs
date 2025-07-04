using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Games
{
    public interface IGameService
    {
        // Basic CRUD
        Task<GameDto?> GetByIdAsync(int id);
        Task<GameDetailDto?> GetGameDetailAsync(int id);
        Task<PagedResultDto<GameListDto>> GetGamesAsync(int pageNumber = 1, int pageSize = 20);
        Task<GameDto> CreateGameAsync(GameCreateDto createDto);
        Task<GameDto> UpdateGameAsync(int id, GameUpdateDto updateDto);
        Task DeleteGameAsync(int id);

        // Game Discovery
        Task<List<GameListDto>> GetFeaturedGamesAsync(int count = 10);
        Task<List<GameListDto>> GetRecentGamesAsync(int count = 10);
        Task<List<GameListDto>> GetTopRatedGamesAsync(int count = 10);
        Task<List<GameListDto>> GetDiscountedGamesAsync();
        Task<List<GameListDto>> GetSimilarGamesAsync(int gameId, int count = 5);

        // Search & Filter
        Task<PagedResultDto<GameListDto>> SearchGamesAsync(string searchTerm, int pageNumber = 1, int pageSize = 20);
        Task<List<GameListDto>> GetGamesByCategoryAsync(int categoryId);
        Task<List<GameListDto>> GetGamesByPlatformAsync(int platformId);
        Task<List<GameListDto>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        // Statistics
        Task<GameStatsDto> GetGameStatsAsync(int gameId);
        Task IncrementGameViewAsync(int gameId);
        Task UpdateGameRatingAsync(int gameId);

        // Admin Operations
        Task<List<GameListDto>> GetGamesForModerationAsync();
        Task ApproveGameAsync(int gameId);
        Task RejectGameAsync(int gameId, string reason);
    }
}
