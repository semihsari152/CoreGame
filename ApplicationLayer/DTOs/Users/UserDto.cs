using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Users
{
    // CoreGame.Application/DTOs/Users/UserDto.cs
    namespace CoreGame.Application.DTOs.Users
    {
        public class UserDto
        {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Bio { get; set; }
            public string? AvatarUrl { get; set; }
            public string? Location { get; set; }
            public int TotalPoints { get; set; }
            public int Level { get; set; }
            public string Role { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public DateTime CreatedDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public DateTime? LastActivityDate { get; set; }
        }

        public class UserListDto
        {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? AvatarUrl { get; set; }
            public int TotalPoints { get; set; }
            public int Level { get; set; }
            public string Role { get; set; } = string.Empty;
            public DateTime? LastActivityDate { get; set; }
            public bool IsOnline { get; set; }
        }

        public class UserProfileDto
        {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Bio { get; set; }
            public string? AvatarUrl { get; set; }
            public string? CoverImageUrl { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string? Location { get; set; }
            public string? Website { get; set; }

            // Social Media
            public string? TwitterHandle { get; set; }
            public string? DiscordTag { get; set; }
            public string? SteamProfileUrl { get; set; }
            public string? TwitchUsername { get; set; }
            public string? YoutubeChannel { get; set; }

            // Gamification
            public int TotalPoints { get; set; }
            public int Level { get; set; }
            public int ExperiencePoints { get; set; }

            // Statistics
            public int TotalGamesPlayed { get; set; }
            public int TotalHoursPlayed { get; set; }
            public int TotalReviewsWritten { get; set; }
            public int TotalCommentsPosted { get; set; }
            public int TotalGuidesCreated { get; set; }

            // Activity
            public decimal ActivityScore { get; set; }
            public int ConsecutiveDays { get; set; }
            public DateTime? LastStreakDate { get; set; }

            // Gaming Preferences
            public string? PreferredDifficulty { get; set; }
            public bool PrefersSinglePlayer { get; set; }
            public bool PrefersMultiplayer { get; set; }
            public bool PrefersCompetitive { get; set; }
            public bool PrefersCoop { get; set; }

            // Settings
            public bool ReceiveEmailNotifications { get; set; }
            public bool ReceivePushNotifications { get; set; }
            public bool IsProfilePublic { get; set; }
            public bool ShowOnlineStatus { get; set; }

            // Achievements
            public List<UserAchievementDto> Achievements { get; set; } = new();
        }

        public class UserCreateDto
        {
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Bio { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string? Location { get; set; }
            public bool AcceptTerms { get; set; }
            public bool ReceiveEmailNotifications { get; set; } = true;
        }

        public class UserUpdateDto
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Bio { get; set; }
            public string? Location { get; set; }
            public string? Website { get; set; }
            public DateTime? DateOfBirth { get; set; }

            // Social Media
            public string? TwitterHandle { get; set; }
            public string? DiscordTag { get; set; }
            public string? SteamProfileUrl { get; set; }
            public string? TwitchUsername { get; set; }
            public string? YoutubeChannel { get; set; }

            // Settings
            public bool ReceiveEmailNotifications { get; set; }
            public bool ReceivePushNotifications { get; set; }
            public bool IsProfilePublic { get; set; }
            public bool ShowOnlineStatus { get; set; }
        }

        public class UserProfileUpdateDto
        {
            // Detaylı Profil Bilgileri
            public string? AboutMe { get; set; }
            public string? FavoriteGenres { get; set; }
            public string? FavoritePlatforms { get; set; }
            public string? GamingExperience { get; set; }

            // Oyun Tercihleri
            public string? PreferredDifficulty { get; set; }
            public bool PrefersSinglePlayer { get; set; }
            public bool PrefersMultiplayer { get; set; }
            public bool PrefersCompetitive { get; set; }
            public bool PrefersCoop { get; set; }

            // Kişiselleştirme
            public string? PreferredTheme { get; set; }
            public string? PreferredLanguage { get; set; }
            public string? TimeZone { get; set; }
        }

        public class UserStatsDto
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;

            // Points & Level
            public int TotalPoints { get; set; }
            public int Level { get; set; }
            public int ExperiencePoints { get; set; }
            public int PointsToNextLevel { get; set; }

            // Activity Stats
            public int TotalGamesPlayed { get; set; }
            public int TotalHoursPlayed { get; set; }
            public int TotalReviewsWritten { get; set; }
            public int TotalCommentsPosted { get; set; }
            public int TotalGuidesCreated { get; set; }
            public int TotalForumPosts { get; set; }

            // Social Stats
            public int FollowersCount { get; set; }
            public int FollowingCount { get; set; }
            public int LikesReceived { get; set; }
            public int LikesGiven { get; set; }

            // Activity Score
            public decimal ActivityScore { get; set; }
            public int ConsecutiveDays { get; set; }
            public DateTime? LastStreakDate { get; set; }

            // Recent Activity
            public DateTime? LastLoginDate { get; set; }
            public DateTime? LastActivityDate { get; set; }

            // Achievements
            public int TotalAchievements { get; set; }
            public int RareAchievements { get; set; }
            public List<UserAchievementDto> RecentAchievements { get; set; } = new();
        }

        public class UserAchievementDto
        {
            public int Id { get; set; }
            public int AchievementId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string? IconUrl { get; set; }
            public string? BadgeUrl { get; set; }
            public string Type { get; set; } = string.Empty;
            public string Rarity { get; set; } = string.Empty;
            public int Points { get; set; }
            public DateTime EarnedDate { get; set; }
            public bool IsDisplayed { get; set; }
            public string? EarnedDescription { get; set; }
        }

        public class LoginDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public bool RememberMe { get; set; }
        }

        public class RegisterDto
        {
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string ConfirmPassword { get; set; } = string.Empty;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public bool AcceptTerms { get; set; }
            public bool ReceiveEmailNotifications { get; set; } = true;
        }

        public class ChangePasswordDto
        {
            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ConfirmNewPassword { get; set; } = string.Empty;
        }

        public class ForgotPasswordDto
        {
            public string Email { get; set; } = string.Empty;
        }

        public class ResetPasswordDto
        {
            public string Token { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ConfirmNewPassword { get; set; } = string.Empty;
        }
    }
}