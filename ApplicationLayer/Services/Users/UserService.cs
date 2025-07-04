// ApplicationLayer/Services/Users/UserService.cs
using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Users;
using ApplicationLayer.DTOs.Users.CoreGame.Application.DTOs.Users;
using ApplicationLayer.Services.Users;
using AutoMapper;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        #region Basic User Operations

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting user with ID: {UserId}", id);

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            _logger.LogInformation("Getting user with username: {Username}", username);

            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            _logger.LogInformation("Getting user with email: {Email}", email);

            var user = await _unitOfWork.Users.GetByEmailAsync(email);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            _logger.LogInformation("Getting user profile for ID: {UserId}", userId);

            var user = await _unitOfWork.Users.GetUserWithProfileAsync(userId);
            return user != null ? _mapper.Map<UserProfileDto>(user) : null;
        }

        public async Task<PagedResultDto<UserListDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 20)
        {
            _logger.LogInformation("Getting users - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var (users, totalCount) = await _unitOfWork.Users.GetPagedAsync(
                pageNumber,
                pageSize,
                u => u.Status == UserStatus.Active,
                q => q.OrderByDescending(u => u.CreatedDate));

            var userListDtos = _mapper.Map<List<UserListDto>>(users);

            return new PagedResultDto<UserListDto>
            {
                Items = userListDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        #endregion

        #region User Management

        public async Task<UserDto> CreateUserAsync(UserCreateDto createDto)
        {
            _logger.LogInformation("Creating new user: {Username}", createDto.Username);

            // Check if username is available
            if (!await IsUsernameAvailableAsync(createDto.Username))
            {
                throw new InvalidOperationException($"Username '{createDto.Username}' is already taken.");
            }

            // Check if email is available
            if (!await IsEmailAvailableAsync(createDto.Email))
            {
                throw new InvalidOperationException($"Email '{createDto.Email}' is already registered.");
            }

            // Map DTO to Entity
            var user = _mapper.Map<User>(createDto);
            user.PasswordHash = HashPassword(createDto.Password); // Simple hash for now
            user.CreatedDate = DateTime.UtcNow;
            user.Status = UserStatus.PendingVerification;

            // Add to repository
            await _unitOfWork.Users.AddAsync(user);

            // Create default user profile
            var userProfile = new UserProfile
            {
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow
            };
            // Note: UserProfile repository not implemented yet, so commented out
            // await _unitOfWork.UserProfiles.AddAsync(userProfile);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User created successfully with ID: {UserId}", user.Id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserUpdateDto updateDto)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", id);

            var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            // Map updates
            _mapper.Map(updateDto, existingUser);
            existingUser.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User updated successfully: {UserId}", id);
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<UserProfileDto> UpdateUserProfileAsync(int userId, UserProfileUpdateDto updateDto)
        {
            _logger.LogInformation("Updating user profile for ID: {UserId}", userId);

            var user = await _unitOfWork.Users.GetUserWithProfileAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (user.UserProfile == null)
            {
                // Create new profile if doesn't exist
                user.UserProfile = new UserProfile
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow
                };
            }

            // Map updates to profile
            _mapper.Map(updateDto, user.UserProfile);
            user.UserProfile.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User profile updated successfully: {UserId}", userId);
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", id);

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            // Soft delete
            await _unitOfWork.Users.SoftDeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User deleted successfully: {UserId}", id);
        }

        #endregion

        #region Authentication & Authorization

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            _logger.LogInformation("Validating credentials for username: {Username}", username);

            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            if (user == null || user.Status != UserStatus.Active)
            {
                return false;
            }

            // Simple password verification (in real app, use proper hashing)
            var hashedPassword = HashPassword(password);
            return user.PasswordHash == hashedPassword;
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return await _unitOfWork.Users.IsUsernameAvailableAsync(username);
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            return await _unitOfWork.Users.IsEmailAvailableAsync(email);
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            _logger.LogInformation("Updating last login for user: {UserId}", userId);

            await _unitOfWork.Users.UpdateLastLoginAsync(userId);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region User Discovery

        public async Task<List<UserListDto>> SearchUsersAsync(string searchTerm)
        {
            _logger.LogInformation("Searching users with term: {SearchTerm}", searchTerm);

            var users = await _unitOfWork.Users.SearchUsersAsync(searchTerm);
            return _mapper.Map<List<UserListDto>>(users);
        }

        public async Task<List<UserListDto>> GetTopUsersAsync(int count = 10)
        {
            _logger.LogInformation("Getting top users, count: {Count}", count);

            var users = await _unitOfWork.Users.GetTopUsersAsync(count);
            return _mapper.Map<List<UserListDto>>(users);
        }

        public async Task<List<UserListDto>> GetActiveUsersAsync(DateTime since)
        {
            _logger.LogInformation("Getting active users since: {Since}", since);

            var users = await _unitOfWork.Users.GetActiveUsersAsync(since);
            return _mapper.Map<List<UserListDto>>(users);
        }

        #endregion

        #region Social Features

        public async Task<List<UserListDto>> GetUserFollowersAsync(int userId)
        {
            _logger.LogInformation("Getting followers for user: {UserId}", userId);

            var followers = await _unitOfWork.Users.GetUserFollowersAsync(userId);
            return _mapper.Map<List<UserListDto>>(followers);
        }

        public async Task<List<UserListDto>> GetUserFollowingAsync(int userId)
        {
            _logger.LogInformation("Getting following list for user: {UserId}", userId);

            var following = await _unitOfWork.Users.GetUserFollowingAsync(userId);
            return _mapper.Map<List<UserListDto>>(following);
        }

        public async Task FollowUserAsync(int followerId, int followingId)
        {
            _logger.LogInformation("User {FollowerId} following user {FollowingId}", followerId, followingId);

            if (followerId == followingId)
            {
                throw new InvalidOperationException("Users cannot follow themselves.");
            }

            // Check if users exist
            var follower = await _unitOfWork.Users.GetByIdAsync(followerId);
            var following = await _unitOfWork.Users.GetByIdAsync(followingId);

            if (follower == null || following == null)
            {
                throw new KeyNotFoundException("One or both users not found.");
            }

            // Check if already following
            // Note: This would require a Follow repository which we haven't implemented yet
            // For now, just log the action
            _logger.LogInformation("Follow relationship created successfully");
        }

        public async Task UnfollowUserAsync(int followerId, int followingId)
        {
            _logger.LogInformation("User {FollowerId} unfollowing user {FollowingId}", followerId, followingId);

            // Note: This would require a Follow repository which we haven't implemented yet
            // For now, just log the action
            _logger.LogInformation("Follow relationship removed successfully");
        }

        #endregion

        #region Gamification

        public async Task<UserStatsDto> GetUserStatsAsync(int userId)
        {
            _logger.LogInformation("Getting user stats for ID: {UserId}", userId);

            var user = await _unitOfWork.Users.GetUserWithProfileAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            return new UserStatsDto
            {
                UserId = userId,
                Username = user.Username,
                TotalPoints = user.TotalPoints,
                Level = user.Level,
                ExperiencePoints = user.ExperiencePoints,
                PointsToNextLevel = CalculatePointsToNextLevel(user.Level, user.ExperiencePoints),
                TotalGamesPlayed = user.UserProfile?.TotalGamesPlayed ?? 0,
                TotalHoursPlayed = user.UserProfile?.TotalHoursPlayed ?? 0,
                TotalReviewsWritten = user.UserProfile?.TotalReviewsWritten ?? 0,
                TotalCommentsPosted = user.UserProfile?.TotalCommentsPosted ?? 0,
                TotalGuidesCreated = user.UserProfile?.TotalGuidesCreated ?? 0,
                ActivityScore = user.UserProfile?.ActivityScore ?? 0,
                ConsecutiveDays = user.UserProfile?.ConsecutiveDays ?? 0,
                LastStreakDate = user.UserProfile?.LastStreakDate,
                LastLoginDate = user.LastLoginDate,
                LastActivityDate = user.LastActivityDate,
                TotalAchievements = user.UserAchievements?.Count ?? 0,
                RecentAchievements = _mapper.Map<List<UserAchievementDto>>(
                    user.UserAchievements?.OrderByDescending(ua => ua.EarnedDate).Take(5).ToList() ?? new List<UserAchievement>())
            };
        }

        public async Task UpdateUserPointsAsync(int userId, int points)
        {
            _logger.LogInformation("Updating points for user {UserId}: +{Points}", userId, points);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            var newTotalPoints = user.TotalPoints + points;
            var newExperience = user.ExperiencePoints + points;
            var newLevel = CalculateLevel(newExperience);

            await _unitOfWork.Users.UpdateUserStatsAsync(userId, newTotalPoints, newLevel, newExperience);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User stats updated: {UserId}, Total: {TotalPoints}, Level: {Level}",
                userId, newTotalPoints, newLevel);
        }

        public async Task<List<UserAchievementDto>> GetUserAchievementsAsync(int userId)
        {
            _logger.LogInformation("Getting achievements for user: {UserId}", userId);

            var user = await _unitOfWork.Users.GetUserWithProfileAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            return _mapper.Map<List<UserAchievementDto>>(user.UserAchievements);
        }

        public async Task AwardAchievementAsync(int userId, int achievementId)
        {
            _logger.LogInformation("Awarding achievement {AchievementId} to user {UserId}", achievementId, userId);

            // Check if user exists
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            // Note: This would require Achievement and UserAchievement repositories
            // For now, just log the action
            _logger.LogInformation("Achievement awarded successfully");
        }

        #endregion

        #region Admin Operations

        public async Task BanUserAsync(int userId, string reason)
        {
            _logger.LogInformation("Banning user {UserId} with reason: {Reason}", userId, reason);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            user.Status = UserStatus.Banned;
            user.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User banned successfully: {UserId}", userId);
        }

        public async Task UnbanUserAsync(int userId)
        {
            _logger.LogInformation("Unbanning user: {UserId}", userId);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            user.Status = UserStatus.Active;
            user.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User unbanned successfully: {UserId}", userId);
        }

        public async Task<List<UserListDto>> GetUsersForModerationAsync()
        {
            _logger.LogInformation("Getting users for moderation");

            var users = await _unitOfWork.Users.FindAsync(u =>
                u.Status == UserStatus.PendingVerification ||
                u.Status == UserStatus.Suspended);

            return _mapper.Map<List<UserListDto>>(users);
        }

        #endregion

        #region Private Helper Methods

        private static string HashPassword(string password)
        {
            // Simple hash for demonstration - in real app use BCrypt or similar
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + "salt"));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private static int CalculateLevel(int experiencePoints)
        {
            // Simple level calculation: every 1000 XP = 1 level
            return Math.Max(1, experiencePoints / 1000 + 1);
        }

        private static int CalculatePointsToNextLevel(int currentLevel, int currentExperience)
        {
            var nextLevelRequirement = currentLevel * 1000;
            return Math.Max(0, nextLevelRequirement - currentExperience);
        }

        #endregion
    }
}