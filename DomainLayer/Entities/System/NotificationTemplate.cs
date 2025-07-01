using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.System
{
    public class NotificationTemplate : BaseEntity
    {
        // Template Bilgileri
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public string EventName { get; set; } = string.Empty; // "UserRegistered", "GameReviewAdded"

        // Template İçerikleri
        public string TitleTemplate { get; set; } = string.Empty; // "{SenderName} yorumunuzu beğendi"
        public string MessageTemplate { get; set; } = string.Empty;
        public string? EmailSubjectTemplate { get; set; }
        public string? EmailBodyTemplate { get; set; }
        public string? PushTitleTemplate { get; set; }
        public string? PushBodyTemplate { get; set; }

        // Ayarlar
        public bool IsActive { get; set; } = true;
        public NotificationPriority DefaultPriority { get; set; } = NotificationPriority.Normal;
        public string? DefaultCategory { get; set; }
        public bool CanBeGrouped { get; set; } = true;
        public int? ExpiryHours { get; set; } // Kaç saat sonra geçersiz

        // Kanallar
        public bool EnableWeb { get; set; } = true;
        public bool EnableEmail { get; set; } = false;
        public bool EnablePush { get; set; } = false;
        public bool EnableSms { get; set; } = false;

        // Sıklık Kontrolü
        public bool HasFrequencyLimit { get; set; } = false;
        public int? MaxPerHour { get; set; }
        public int? MaxPerDay { get; set; }

        // Hedef Kullanıcı Filtresi
        public string? TargetRoles { get; set; } // JSON array
        public string? TargetUserSettings { get; set; } // JSON - hangi ayarları olan kullanıcılar

        // Navigation Properties
        public virtual ICollection<NotificationTemplateAction> TemplateActions { get; set; } = new List<NotificationTemplateAction>();
    }
}
