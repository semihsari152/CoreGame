using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Forum
{
    public class PostHistory : BaseEntity
    {
        public int PostId { get; set; }
        public int EditedBy { get; set; }
        public string PreviousContent { get; set; } = string.Empty;
        public string EditReason { get; set; } = string.Empty;
        public PostEditType EditType { get; set; }

        // Navigation Properties
        public virtual ForumPost Post { get; set; } = null!;
        public virtual User Editor { get; set; } = null!;
    }
}
