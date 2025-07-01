using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class ReviewHistory : BaseEntity
    {
        public int ReviewId { get; set; }
        public int EditedBy { get; set; }

        // Önceki Veriler
        public string? PreviousTitle { get; set; }
        public string? PreviousContent { get; set; }
        public decimal? PreviousOverallRating { get; set; }
        public bool? PreviousIsRecommended { get; set; }

        // Edit Bilgileri
        public string EditReason { get; set; } = string.Empty;
        public ReviewEditType EditType { get; set; }

        // Navigation Properties
        public virtual GameReview Review { get; set; } = null!;
        public virtual User Editor { get; set; } = null!;
    }
}
