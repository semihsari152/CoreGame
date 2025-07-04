using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Users.CoreGame.Application.DTOs.Users;
using ApplicationLayer.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDto> _userCreateValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            IValidator<UserCreateDto> userCreateValidator,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _userCreateValidator = userCreateValidator;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _logger = logger;
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<UserDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = $"User with ID {id} not found"
                    });
                }

                return Ok(new ApiResponseDto<UserDto>
                {
                    Success = true,
                    Data = user,
                    Message = "User retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {UserId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        [HttpGet("{id}/profile")]
        [ProducesResponseType(typeof(ApiResponseDto<UserProfileDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 404)]
        public async Task<ActionResult<ApiResponseDto<UserProfileDto>>> GetUserProfile(int id)
        {
            try
            {
                var profile = await _userService.GetUserProfileAsync(id);
                if (profile == null)
                {
                    return NotFound(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = $"User profile with ID {id} not found"
                    });
                }

                return Ok(new ApiResponseDto<UserProfileDto>
                {
                    Success = true,
                    Data = profile,
                    Message = "User profile retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user profile for ID: {UserId}", id);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the user profile",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponseDto<UserDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                // Validate input
                var validationResult = await _registerValidator.ValidateAsync(registerDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                // Convert RegisterDto to UserCreateDto
                var userCreateDto = new UserCreateDto
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    DateOfBirth = registerDto.DateOfBirth,
                    AcceptTerms = registerDto.AcceptTerms,
                    ReceiveEmailNotifications = registerDto.ReceiveEmailNotifications
                };

                var user = await _userService.CreateUserAsync(userCreateDto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new ApiResponseDto<UserDto>
                {
                    Success = true,
                    Data = user,
                    Message = "User registered successfully"
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
                _logger.LogError(ex, "Error registering user");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while registering the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// User login
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 401)]
        public async Task<ActionResult<ApiResponseDto<object>>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Validate input
                var validationResult = await _loginValidator.ValidateAsync(loginDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                var isValid = await _userService.ValidateUserCredentialsAsync(loginDto.Username, loginDto.Password);
                if (!isValid)
                {
                    return Unauthorized(new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    });
                }

                // Here you would typically generate JWT token
                // For now, just return success
                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Message = "Login successful",
                    Data = new { Token = "jwt-token-here", ExpiresAt = DateTime.UtcNow.AddHours(24) }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred during login",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Check username availability
        /// </summary>
        [HttpGet("check-username/{username}")]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 200)]
        public async Task<ActionResult<ApiResponseDto<object>>> CheckUsernameAvailability(string username)
        {
            try
            {
                var isAvailable = await _userService.IsUsernameAvailableAsync(username);
                return Ok(new ApiResponseDto<object>
                {
                    Success = true,
                    Data = new { IsAvailable = isAvailable },
                    Message = isAvailable ? "Username is available" : "Username is already taken"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking username availability");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while checking username availability",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Search users
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponseDto<List<UserListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<List<UserListDto>>>> SearchUsers([FromQuery] string searchTerm)
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

                var users = await _userService.SearchUsersAsync(searchTerm);
                return Ok(new ApiResponseDto<List<UserListDto>>
                {
                    Success = true,
                    Data = users,
                    Message = "User search completed successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching users with term: {SearchTerm}", searchTerm);
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while searching users",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get top users
        /// </summary>
        [HttpGet("top")]
        [ProducesResponseType(typeof(ApiResponseDto<List<UserListDto>>), 200)]
        public async Task<ActionResult<ApiResponseDto<List<UserListDto>>>> GetTopUsers([FromQuery] int count = 10)
        {
            try
            {
                var users = await _userService.GetTopUsersAsync(count);
                return Ok(new ApiResponseDto<List<UserListDto>>
                {
                    Success = true,
                    Data = users,
                    Message = "Top users retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top users");
                return StatusCode(500, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving top users",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
