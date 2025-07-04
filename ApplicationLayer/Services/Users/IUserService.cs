using ApplicationLayer.DTOs.Common;
using ApplicationLayer.DTOs.Users.CoreGame.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Users
{
    public interface IUserService
    {
        // Basic User Operations
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<PagedResultDto<UserListDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 20);

        // User Management
        Task<UserDto> CreateUserAsync(UserCreateDto createDto);
        Task<UserDto> UpdateUserAsync(int id, UserUpdateDto updateDto);
        Task<UserProfileDto> UpdateUserProfileAsync(int userId, UserProfileUpdateDto updateDto);
        Task DeleteUserAsync(int id);

        // Authentication & Authorization
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
        Task UpdateLastLoginAsync(int userId);

        // User Discovery
        Task<List<UserListDto>> SearchUsersAsync(string searchTerm);
        Task<List<UserListDto>> GetTopUsersAsync(int count = 10);
        Task<List<UserListDto>> GetActiveUsersAsync(DateTime since);

        // Social Features
        Task<List<UserListDto>> GetUserFollowersAsync(int userId);
        Task<List<UserListDto>> GetUserFollowingAsync(int userId);
        Task FollowUserAsync(int followerId, int followingId);
        Task UnfollowUserAsync(int followerId, int followingId);

        // Gamification
        Task<UserStatsDto> GetUserStatsAsync(int userId);
        Task UpdateUserPointsAsync(int userId, int points);
        Task<List<UserAchievementDto>> GetUserAchievementsAsync(int userId);
        Task AwardAchievementAsync(int userId, int achievementId);

        // Admin Operations
        Task BanUserAsync(int userId, string reason);
        Task UnbanUserAsync(int userId);
        Task<List<UserListDto>> GetUsersForModerationAsync();
    }
}
