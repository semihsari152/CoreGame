using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Social
{
    public class Like : BaseEntity
    {
        public int UserId { get; set; }
        public LikeableType LikeableType { get; set; }
        public int LikeableId { get; set; }
        public LikeType Type { get; set; } = LikeType.Like; // Like or Dislike

        // Navigation Properties
        public virtual User User { get; set; } = null!;
    }
}
