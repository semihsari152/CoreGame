using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class ReviewHelpful : BaseEntity
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public bool IsHelpful { get; set; } = true; // true=helpful, false=unhelpful

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual GameReview Review { get; set; } = null!;
    }
}
