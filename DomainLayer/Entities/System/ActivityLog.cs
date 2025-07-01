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
    public class ActivityLog : BaseEntity
    {
        // Kullanıcı ve Aktivite
        public int? UserId { get; set; }
        public string ActivityType { get; set; } = string.Empty; // "GameAdded", "CommentPosted"
        public string Description { get; set; } = string.Empty;

        // Entity Bilgileri
        public string? EntityType { get; set; }
        public int? EntityId { get; set; }
        public string? EntityTitle { get; set; }

        // Teknik Bilgiler
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? SessionId { get; set; }

        // Ek Veriler
        public string? Metadata { get; set; } // JSON
        public LogLevel Level { get; set; } = LogLevel.Information;

        // Durum
        public bool IsVisible { get; set; } = true; // Kullanıcı profilinde görünsün mü
        public bool IsPublic { get; set; } = false; // Herkese açık mı

        // Navigation Properties
        public virtual User? User { get; set; }
    }
}
