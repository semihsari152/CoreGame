using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class GuideRating : BaseEntity
    {
        public int GuideId { get; set; }
        public int UserId { get; set; }

        // Puanlama
        public decimal Rating { get; set; } // 1-5 yıldız
        public bool IsHelpful { get; set; } = true;
        public string? Review { get; set; }

        // Kategori Puanları
        public decimal? ClarityRating { get; set; } // Anlaşılırlık
        public decimal? AccuracyRating { get; set; } // Doğruluk
        public decimal? CompletenessRating { get; set; } // Eksiksizlik

        // Navigation Properties
        public virtual Guide Guide { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
