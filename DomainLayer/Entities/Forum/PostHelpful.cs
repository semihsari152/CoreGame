using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Forum
{
    public class PostHelpful : BaseEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsHelpful { get; set; } = true;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ForumPost Post { get; set; } = null!;
    }
}
