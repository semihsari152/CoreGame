using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class ReviewTemplate : BaseEntity
    {
        // Template Bilgileri
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ReviewType ReviewType { get; set; }

        // Template İçeriği
        public string Template { get; set; } = string.Empty; // Markdown template
        public string? GuideText { get; set; } // Kullanıcıya yardım metni

        // Kategoriler
        public string? GameGenres { get; set; } // Hangi oyun türleri için uygun
        public bool IsActive { get; set; } = true;
        public bool IsDefault { get; set; } = false;

        // Kullanım İstatistikleri
        public int UsageCount { get; set; } = 0;
        public int CreatedBy { get; set; }

        // Navigation Properties
        public virtual User Creator { get; set; } = null!;
    }
}
