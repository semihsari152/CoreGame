using DomainLayer.Common;
using DomainLayer.Entities.Games;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.System;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DomainLayer.Entities.Users
{
    public class User : BaseEntity
    {
        // Temel Kimlik Bilgileri
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Profil Bilgileri
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Website { get; set; }

        // Sosyal Medya
        public string? TwitterHandle { get; set; }
        public string? DiscordTag { get; set; }
        public string? SteamProfileUrl { get; set; }
        public string? TwitchUsername { get; set; }
        public string? YoutubeChannel { get; set; }

        // Hesap Durumu
        public UserRole Role { get; set; } = UserRole.User;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsPhoneConfirmed { get; set; } = false;
        public string? PhoneNumber { get; set; }

        // Aktivite ve İstatistikler
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public int TotalPoints { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int ExperiencePoints { get; set; } = 0;

        // Ayarlar
        public bool ReceiveEmailNotifications { get; set; } = true;
        public bool ReceivePushNotifications { get; set; } = true;
        public bool IsProfilePublic { get; set; } = true;
        public bool ShowOnlineStatus { get; set; } = true;

        // Güvenlik
        public string? SecurityStamp { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; } = 0;
        public bool TwoFactorEnabled { get; set; } = false;

        // Navigation Properties
        public virtual UserProfile? UserProfile { get; set; }
        public virtual ICollection<UserGameList> UserGameLists { get; set; } = new List<UserGameList>();
        public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<GameReview> GameReviews { get; set; } = new List<GameReview>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Follow> Following { get; set; } = new List<Follow>();
        public virtual ICollection<Follow> Followers { get; set; } = new List<Follow>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
