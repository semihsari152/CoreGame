using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class ReviewAward : BaseEntity
    {
        public int ReviewId { get; set; }
        public int AwardId { get; set; }
        public int AwardedBy { get; set; }
        public DateTime AwardedDate { get; set; }
        public string? Reason { get; set; }

        // Navigation Properties
        public virtual GameReview Review { get; set; } = null!;
        public virtual Award Award { get; set; } = null!;
        public virtual User AwardedByUser { get; set; } = null!;
    }
}
