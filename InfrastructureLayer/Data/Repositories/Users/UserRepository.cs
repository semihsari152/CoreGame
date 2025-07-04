using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.Data.Context;
using InfrastructureLayer.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CoreGameDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserWithProfileAsync(int userId)
        {
            return await _dbSet
                .Include(u => u.UserProfile)
                .Include(u => u.UserAchievements)
                .ThenInclude(ua => ua.Achievement)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _dbSet
                .Where(u => u.Role == role && u.Status == UserStatus.Active)
                .OrderBy(u => u.Username)
                .ToListAsync();
        }

        public async Task<List<User>> SearchUsersAsync(string searchTerm)
        {
            var term = searchTerm.ToLower().Trim();

            return await _dbSet
                .Where(u =>
                    u.Username.ToLower().Contains(term) ||
                    u.FirstName.ToLower().Contains(term) ||
                    u.LastName.ToLower().Contains(term) ||
                    u.Email.ToLower().Contains(term))
                .OrderBy(u => u.Username)
                .ToListAsync();
        }

        public async Task<List<User>> GetTopUsersAsync(int count = 10)
        {
            return await _dbSet
                .Include(u => u.UserProfile)
                .Where(u => u.Status == UserStatus.Active)
                .OrderByDescending(u => u.TotalPoints)
                .ThenByDescending(u => u.Level)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<User>> GetActiveUsersAsync(DateTime since)
        {
            return await _dbSet
                .Where(u => u.LastActivityDate >= since && u.Status == UserStatus.Active)
                .OrderByDescending(u => u.LastActivityDate)
                .ToListAsync();
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return !await _dbSet.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.UtcNow;
                user.LastActivityDate = DateTime.UtcNow;
                Update(user);
            }
        }

        public async Task UpdateUserStatsAsync(int userId, int totalPoints, int level, int experiencePoints)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.TotalPoints = totalPoints;
                user.Level = level;
                user.ExperiencePoints = experiencePoints;
                Update(user);
            }
        }

        public async Task<List<User>> GetUserFollowersAsync(int userId)
        {
            return await _dbSet
                .Where(u => u.Following.Any(f => f.FollowingId == userId))
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<User>> GetUserFollowingAsync(int userId)
        {
            return await _dbSet
                .Where(u => u.Followers.Any(f => f.FollowerId == userId))
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetUserLevelsAsync(List<int> userIds)
        {
            return await _dbSet
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Level);
        }
    }
}
