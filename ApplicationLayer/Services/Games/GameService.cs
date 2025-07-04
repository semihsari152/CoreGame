using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Games;
using AutoMapper;
using DomainLayer.Entities.Games;
using DomainLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Games
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GameService> _logger;

        public GameService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GameService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        #region Basic CRUD

        public async Task<GameDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting game with ID: {GameId}", id);

            var game = await _unitOfWork.Games.GetByIdAsync(id);
            return game != null ? _mapper.Map<GameDto>(game) : null;
        }

        public async Task<GameDetailDto?> GetGameDetailAsync(int id)
        {
            _logger.LogInformation("Getting game detail with ID: {GameId}", id);

            var game = await _unitOfWork.Games.GetGameWithDetailsAsync(id);
            if (game == null)
                return null;

            // Increment view count
            await IncrementGameViewAsync(id);

            return _mapper.Map<GameDetailDto>(game);
        }

        public async Task<PagedResultDto<GameListDto>> GetGamesAsync(int pageNumber = 1, int pageSize = 20)
        {
            _logger.LogInformation("Getting games - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var (games, totalCount) = await _unitOfWork.Games.GetPagedAsync(
                pageNumber,
                pageSize,
                g => g.Status == GameStatus.Published,
                q => q.OrderByDescending(g => g.CreatedDate));

            var gameListDtos = _mapper.Map<List<GameListDto>>(games);

            return new PagedResultDto<GameListDto>
            {
                Items = gameListDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<GameDto> CreateGameAsync(GameCreateDto createDto)
        { 
_logger.LogInformation("Creating new game: {GameTitle}", createDto.Title);
    
    // Validation
    if (await _unitOfWork.Games.ExistsAsync(g => g.Title == createDto.Title))
    {
        throw new InvalidOperationException($"Game with title '{createDto.Title}' already exists.");
    }

    // Map DTO to Entity (sadece temel alanlar)
    var game = new Game
    {
        Title = createDto.Title,
        Description = createDto.Description,
        ShortDescription = createDto.ShortDescription,
        Publisher = createDto.Publisher,
        Developer = createDto.Developer,
        Price = createDto.Price,
        CoverImageUrl = createDto.CoverImageUrl,
        ReleaseDate = createDto.ReleaseDate,
        Status = GameStatus.Draft,
        CreatedDate = DateTime.UtcNow,
        AverageRating = 0,
        TotalRatings = 0,
        ViewCount = 0,
        DownloadCount = 0,
        DiscountPercentage = 0,
        IsFree = false
    };

        // Add to repository
        await _unitOfWork.Games.AddAsync(game);
        await _unitOfWork.SaveChangesAsync();

    _logger.LogInformation("Game created successfully with ID: {GameId}", game.Id);
    return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> UpdateGameAsync(int id, GameUpdateDto updateDto)
        {
            _logger.LogInformation("Updating game with ID: {GameId}", id);

            var existingGame = await _unitOfWork.Games.GetByIdAsync(id);
            if (existingGame == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }

            // Map updates
            _mapper.Map(updateDto, existingGame);
            existingGame.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Games.Update(existingGame);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Game updated successfully: {GameId}", id);
            return _mapper.Map<GameDto>(existingGame);
        }

        public async Task DeleteGameAsync(int id)
        {
            _logger.LogInformation("Deleting game with ID: {GameId}", id);

            var game = await _unitOfWork.Games.GetByIdAsync(id);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }

            // Soft delete
            await _unitOfWork.Games.SoftDeleteAsync(game);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Game deleted successfully: {GameId}", id);
        }

        #endregion

        #region Game Discovery

        public async Task<List<GameListDto>> GetFeaturedGamesAsync(int count = 10)
        {
            _logger.LogInformation("Getting featured games, count: {Count}", count);

            var games = await _unitOfWork.Games.GetFeaturedGamesAsync(count);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetRecentGamesAsync(int count = 10)
        {
            _logger.LogInformation("Getting recent games, count: {Count}", count);

            var games = await _unitOfWork.Games.GetRecentlyAddedGamesAsync(count);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetTopRatedGamesAsync(int count = 10)
        {
            _logger.LogInformation("Getting top rated games, count: {Count}", count);

            var games = await _unitOfWork.Games.GetTopRatedGamesAsync(count);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetDiscountedGamesAsync()
        {
            _logger.LogInformation("Getting discounted games");

            var games = await _unitOfWork.Games.GetDiscountedGamesAsync();
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetSimilarGamesAsync(int gameId, int count = 5)
        {
            _logger.LogInformation("Getting similar games for GameId: {GameId}, count: {Count}", gameId, count);

            var games = await _unitOfWork.Games.GetSimilarGamesAsync(gameId, count);
            return _mapper.Map<List<GameListDto>>(games);
        }

        #endregion

        #region Search & Filter

        public async Task<PagedResultDto<GameListDto>> SearchGamesAsync(string searchTerm, int pageNumber = 1, int pageSize = 20)
        {
            _logger.LogInformation("Searching games with term: {SearchTerm}", searchTerm);

            var games = await _unitOfWork.Games.SearchGamesAsync(searchTerm);
            var totalCount = games.Count;

            var pagedGames = games
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var gameListDtos = _mapper.Map<List<GameListDto>>(pagedGames);

            return new PagedResultDto<GameListDto>
            {
                Items = gameListDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<List<GameListDto>> GetGamesByCategoryAsync(int categoryId)
        {
            _logger.LogInformation("Getting games by category: {CategoryId}", categoryId);

            var games = await _unitOfWork.Games.GetGamesByCategoryAsync(categoryId);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetGamesByPlatformAsync(int platformId)
        {
            _logger.LogInformation("Getting games by platform: {PlatformId}", platformId);

            var games = await _unitOfWork.Games.GetGamesByPlatformAsync(platformId);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task<List<GameListDto>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            _logger.LogInformation("Getting games by price range: {MinPrice}-{MaxPrice}", minPrice, maxPrice);

            var games = await _unitOfWork.Games.GetGamesByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<List<GameListDto>>(games);
        }

        #endregion

        #region Statistics

        public async Task<GameStatsDto> GetGameStatsAsync(int gameId)
        {
            _logger.LogInformation("Getting game stats for GameId: {GameId}", gameId);

            var game = await _unitOfWork.Games.GetByIdAsync(gameId);
            if (game == null)
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");

            // Get additional stats
            var reviewCount = await _unitOfWork.Games.CountAsync(g => g.Id == gameId); // This should be review repository
            var commentCounts = await _unitOfWork.Comments.GetCommentCountsAsync(CommentableType.Game, new List<int> { gameId });

            return new GameStatsDto
            {
                GameId = gameId,
                ViewCount = game.ViewCount,
                AverageRating = game.AverageRating,
                TotalRatings = game.TotalRatings,
                ReviewCount = reviewCount,
                CommentCount = commentCounts.GetValueOrDefault(gameId, 0)
            };
        }

        public async Task IncrementGameViewAsync(int gameId)
        {
            try
            {
                var game = await _unitOfWork.Games.GetByIdAsync(gameId);
                if (game != null)
                {
                    game.ViewCount++;
                    _unitOfWork.Games.Update(game);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error incrementing view count for game {GameId}", gameId);
                // Don't throw - view count increment shouldn't break the main flow
            }
        }

        public async Task UpdateGameRatingAsync(int gameId)
        {
            _logger.LogInformation("Updating game rating for GameId: {GameId}", gameId);

            // This would typically calculate average from reviews
            // For now, just a placeholder
            var game = await _unitOfWork.Games.GetByIdAsync(gameId);
            if (game != null)
            {
                // Calculate new rating from reviews (placeholder)
                // var reviews = await _unitOfWork.GameReviews.GetByGameIdAsync(gameId);
                // var newRating = reviews.Average(r => r.OverallRating);
                // var ratingCount = reviews.Count;

                // await _unitOfWork.Games.UpdateGameRatingAsync(gameId, newRating, ratingCount);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region Admin Operations

        public async Task<List<GameListDto>> GetGamesForModerationAsync()
        {
            _logger.LogInformation("Getting games for moderation");

            var games = await _unitOfWork.Games.FindAsync(g => g.Status == GameStatus.Draft || g.Status == GameStatus.ComingSoon);
            return _mapper.Map<List<GameListDto>>(games);
        }

        public async Task ApproveGameAsync(int gameId)
        {
            _logger.LogInformation("Approving game with ID: {GameId}", gameId);

            var game = await _unitOfWork.Games.GetByIdAsync(gameId);
            if (game == null)
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");

            game.Status = GameStatus.Published;
            game.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Game approved successfully: {GameId}", gameId);
        }

        public async Task RejectGameAsync(int gameId, string reason)
        {
            _logger.LogInformation("Rejecting game with ID: {GameId}, Reason: {Reason}", gameId, reason);

            var game = await _unitOfWork.Games.GetByIdAsync(gameId);
            if (game == null)
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");

            game.Status = GameStatus.Archived;
            game.UpdatedDate = DateTime.UtcNow;
            // You might want to store the rejection reason somewhere

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Game rejected successfully: {GameId}", gameId);
        }

        #endregion
    }
}
