using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Users
{
    public class Achievement : BaseEntity
    {
        // Temel Bilgiler
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string? BadgeUrl { get; set; }

        // Achievement Özellikleri
        public AchievementType Type { get; set; }
        public AchievementRarity Rarity { get; set; }
        public int Points { get; set; } = 0;
        public int RequiredValue { get; set; } = 1; // Kaç kez yapılması gerekiyor

        // Durum
        public bool IsActive { get; set; } = true;
        public bool IsSecret { get; set; } = false; // Gizli achievement mi
        public DateTime? ExpiryDate { get; set; } // Sınırlı zamanlı achievement

        // Kategorize
        public string? Category { get; set; }
        public int DisplayOrder { get; set; } = 0;

        // Navigation Properties
        public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
