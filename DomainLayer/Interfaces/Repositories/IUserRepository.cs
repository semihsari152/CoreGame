using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        // User-specific methods
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetUserWithProfileAsync(int userId);
        Task<List<User>> GetUsersByRoleAsync(UserRole role);
        Task<List<User>> SearchUsersAsync(string searchTerm);
        Task<List<User>> GetTopUsersAsync(int count = 10);
        Task<List<User>> GetActiveUsersAsync(DateTime since);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
        Task UpdateLastLoginAsync(int userId);
        Task UpdateUserStatsAsync(int userId, int totalPoints, int level, int experiencePoints);
        Task<List<User>> GetUserFollowersAsync(int userId);
        Task<List<User>> GetUserFollowingAsync(int userId);
        Task<Dictionary<int, int>> GetUserLevelsAsync(List<int> userIds);
    }
}
