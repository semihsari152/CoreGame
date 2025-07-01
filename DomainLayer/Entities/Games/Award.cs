using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class Award : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string? BadgeUrl { get; set; }

        // Award Özellikleri
        public AwardType Type { get; set; }
        public string ColorCode { get; set; } = "#FFD700";
        public int Points { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        // Criteria
        public string? Criteria { get; set; } // JSON - ödül kriterleri

        // Navigation Properties
        public virtual ICollection<ReviewAward> ReviewAwards { get; set; } = new List<ReviewAward>();
    }
}
