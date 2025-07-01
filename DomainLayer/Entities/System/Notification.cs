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
    public class Notification : BaseEntity
    {
        // Temel Bilgiler
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;

        // Durum
        public bool IsRead { get; set; } = false;
        public DateTime? ReadDate { get; set; }
        public bool IsDelivered { get; set; } = false;
        public DateTime? DeliveredDate { get; set; }
        public bool IsClicked { get; set; } = false;
        public DateTime? ClickedDate { get; set; }

        // İçerik ve Linkler
        public string? ActionUrl { get; set; } // Tıklayınca nereye gidecek
        public string? ImageUrl { get; set; }
        public string? IconUrl { get; set; }

        // İlgili Entity Bilgileri (Polymorphic)
        public string? RelatedEntityType { get; set; } // "Game", "Comment", "ForumTopic" vs.
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityTitle { get; set; } // Cache için

        // Gönderen Bilgileri
        public int? SenderId { get; set; } // Kim gönderdi (null = sistem)
        public string? SenderName { get; set; } // Cache için
        public string? SenderAvatarUrl { get; set; } // Cache için

        // Bildirim Kanalları
        public bool SentViaWeb { get; set; } = true;
        public bool SentViaEmail { get; set; } = false;
        public bool SentViaPush { get; set; } = false;
        public bool SentViaSms { get; set; } = false;

        // Gönderim Durumları
        public NotificationStatus WebStatus { get; set; } = NotificationStatus.Pending;
        public NotificationStatus EmailStatus { get; set; } = NotificationStatus.NotSent;
        public NotificationStatus PushStatus { get; set; } = NotificationStatus.NotSent;
        public NotificationStatus SmsStatus { get; set; } = NotificationStatus.NotSent;

        // Zamanlama
        public DateTime? ScheduledDate { get; set; } // Zamanlanmış bildirim
        public DateTime? ExpiryDate { get; set; } // Son geçerlilik tarihi

        // Grup ve Kategori
        public string? Category { get; set; } // "Achievement", "SocialInteraction", "GameUpdate"
        public string? GroupKey { get; set; } // Aynı grup bildirimleri birleştirmek için
        public bool CanBeGrouped { get; set; } = true;

        // Metadata
        public string? Metadata { get; set; } // JSON - ek bilgiler
        public string? Tags { get; set; } // Virgülle ayrılmış etiketler

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual User? Sender { get; set; }
        public virtual ICollection<NotificationAction> NotificationActions { get; set; } = new List<NotificationAction>();
    }
}
