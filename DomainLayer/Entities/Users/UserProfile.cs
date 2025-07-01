using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Users
{
    public class UserProfile : BaseEntity
    {
        public int UserId { get; set; }

        // Detaylı Profil Bilgileri
        public string? AboutMe { get; set; }
        public string? FavoriteGenres { get; set; } // JSON array
        public string? FavoritePlatforms { get; set; } // JSON array
        public string? GamingExperience { get; set; } // Beginner, Intermediate, Expert

        // İstatistikler
        public int TotalGamesPlayed { get; set; } = 0;
        public int TotalHoursPlayed { get; set; } = 0;
        public int TotalReviewsWritten { get; set; } = 0;
        public int TotalCommentsPosted { get; set; } = 0;
        public int TotalGuidesCreated { get; set; } = 0;

        // Aktivite Skoru
        public decimal ActivityScore { get; set; } = 0;
        public int ConsecutiveDays { get; set; } = 0;
        public DateTime? LastStreakDate { get; set; }

        // Oyun Tercihleri
        public string? PreferredDifficulty { get; set; }
        public bool PrefersSinglePlayer { get; set; } = true;
        public bool PrefersMultiplayer { get; set; } = true;
        public bool PrefersCompetitive { get; set; } = false;
        public bool PrefersCoop { get; set; } = true;

        // Kişiselleştirme
        public string? PreferredTheme { get; set; } = "Dark";
        public string? PreferredLanguage { get; set; } = "en";
        public string? TimeZone { get; set; }

        // Navigation Property
        public virtual User User { get; set; } = null!;
    }
}
