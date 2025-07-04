using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Games;
using ApplicationLayer.Services.Games;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IValidator<GameCreateDto> _gameCreateValidator;
        private readonly IValidator<GameUpdateDto> _gameUpdateValidator;
        private readonly ILogger<GamesController> _logger;

        public GamesController(
            IGameService gameService,
            IValidator<GameCreateDto> gameCreateValidator,
            IValidator<GameUpdateDto> gameUpdateValidator,
            ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _gameCreateValidator = gameCreateValidator;
            _gameUpdateValidator = gameUpdateValidator;
            _logger = logger;
        }

        /// <summary>
        /// Get all games with pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<GameListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<PagedResultDto<GameListDto>>>> GetGames(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _gameService.GetGamesAsync(pageNumber, pageSize);
                return Ok(new ApiResponseDto<PagedResultDto<GameListDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Games retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving games");
                return StatusCode(500, new ApiResponseDto<PagedResultDto<GameListDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving games",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get game by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<GameDetailDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<GameDetailDto>>> GetGame(int id)
        {
            try
            {
                var game = await _gameService.GetGameDetailAsync(id);
                if (game == null)
                {
                    return NotFound(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = $"Game with ID {id} not found"
                    });
                }

                return Ok(new ApiResponseDto<GameDetailDto>
                {
                    Success = true,
                    Data = game,
                    Message = "Game retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving game with ID: {GameId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the game",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<GameDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        public async Task<ActionResult<ApiResponseDto<GameDto>>> CreateGame([FromBody] GameCreateDto createDto)
        {
            try
            {
                // Validate input
                var validationResult = await _gameCreateValidator.ValidateAsync(createDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                var game = await _gameService.CreateGameAsync(createDto);
                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, new ApiResponseDto<GameDto>
                {
                    Success = true,
                    Data = game,
                    Message = "Game created successfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating game");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while creating the game",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Update an existing game
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<GameDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<GameDto>>> UpdateGame(int id, [FromBody] GameUpdateDto updateDto)
        {
            try
            {
                // Validate input
                var validationResult = await _gameUpdateValidator.ValidateAsync(updateDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                var game = await _gameService.UpdateGameAsync(id, updateDto);
                return Ok(new ApiResponseDto<GameDto>
                {
                    Success = true,
                    Data = game,
                    Message = "Game updated successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating game with ID: {GameId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while updating the game",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<object>>> DeleteGame(int id)
        {
            try
            {
                await _gameService.DeleteGameAsync(id);
                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Message = "Game deleted successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting game with ID: {GameId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the game",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Search games
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<GameListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<PagedResultDto<GameListDto>>>> SearchGames(
            [FromQuery] string searchTerm,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Search term is required"
                    });
                }

                var result = await _gameService.SearchGamesAsync(searchTerm, pageNumber, pageSize);
                return Ok(new ApiResponseDto<PagedResultDto<GameListDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Search completed successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching games with term: {SearchTerm}", searchTerm);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while searching games",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get featured games
        /// </summary>
        [HttpGet("featured")]
        [ProducesResponseType(typeof(ApiResponseDto<List<GameListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<List<GameListDto>>>> GetFeaturedGames([FromQuery] int count = 10)
        {
            try
            {
                var games = await _gameService.GetFeaturedGamesAsync(count);
                return Ok(new ApiResponseDto<List<GameListDto>>
                {
                    Success = true,
                    Data = games,
                    Message = "Featured games retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving featured games");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving featured games",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get games by category
        /// </summary>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(ApiResponseDto<List<GameListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<List<GameListDto>>>> GetGamesByCategory(int categoryId)
        {
            try
            {
                var games = await _gameService.GetGamesByCategoryAsync(categoryId);
                return Ok(new ApiResponseDto<List<GameListDto>>
                {
                    Success = true,
                    Data = games,
                    Message = "Games retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving games by category: {CategoryId}", categoryId);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving games",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get game statistics
        /// </summary>
        [HttpGet("{id}/stats")]
        [ProducesResponseType(typeof(ApiResponseDto<GameStatsDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<GameStatsDto>>> GetGameStats(int id)
        {
            try
            {
                var stats = await _gameService.GetGameStatsAsync(id);
                return Ok(new ApiResponseDto<GameStatsDto>
                {
                    Success = true,
                    Data = stats,
                    Message = "Game statistics retrieved successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving game stats for ID: {GameId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving game statistics",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
