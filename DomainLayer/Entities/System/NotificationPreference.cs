using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.System
{
    public class NotificationPreference : BaseEntity
    {
        public int UserId { get; set; }

        // Genel Ayarlar
        public bool IsEnabled { get; set; } = true;
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = true;
        public bool SmsNotifications { get; set; } = false;

        // Kategori Bazlı Ayarlar
        public bool GameUpdates { get; set; } = true;
        public bool SocialInteractions { get; set; } = true;
        public bool ForumActivity { get; set; } = true;
        public bool AchievementUnlocked { get; set; } = true;
        public bool ReviewInteractions { get; set; } = true;
        public bool FriendActivity { get; set; } = true;
        public bool SystemAnnouncements { get; set; } = true;
        public bool SecurityAlerts { get; set; } = true;
        public bool MarketingEmails { get; set; } = false;
        public bool WeeklyDigest { get; set; } = true;

        // Zamanlama Ayarları
        public bool QuietHoursEnabled { get; set; } = false;
        public TimeSpan? QuietHoursStart { get; set; }
        public TimeSpan? QuietHoursEnd { get; set; }
        public string? TimeZone { get; set; }

        // Sıklık Ayarları
        public DigestFrequency DigestFrequency { get; set; } = DigestFrequency.Daily;
        public bool GroupSimilarNotifications { get; set; } = true;
        public int MaxNotificationsPerHour { get; set; } = 10;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
    }
}
