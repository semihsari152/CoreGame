using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class Media : BaseEntity
    {
        // Temel Bilgiler
        public string FileName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public int UploadedBy { get; set; }

        // Dosya Özellikleri
        public MediaType Type { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public long FileSize { get; set; } // Byte cinsinden
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Duration { get; set; } // Video/Audio için saniye

        // Metadata
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }

        // İlişkili Entity (Polymorphic)
        public string? EntityType { get; set; } // "Guide", "BlogPost", "Game"
        public int? EntityId { get; set; }

        // Durum ve Moderasyon
        public MediaStatus Status { get; set; } = MediaStatus.Active;
        public bool IsPublic { get; set; } = true;
        public bool RequiresModeration { get; set; } = false;

        // Kullanım İstatistikleri
        public int ViewCount { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;

        // Storage Bilgileri
        public string? StorageProvider { get; set; } // "Local", "AWS", "Azure"
        public string? StorageKey { get; set; }
        public string? CDNUrl { get; set; }

        // Navigation Properties
        public virtual User UploadedByUser { get; set; } = null!;
    }
}
