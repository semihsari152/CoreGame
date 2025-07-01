using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Social
{
    public class CommentHistory : BaseEntity
    {
        public int CommentId { get; set; }
        public string PreviousContent { get; set; } = string.Empty;
        public string EditReason { get; set; } = string.Empty;
        public int EditedBy { get; set; }

        // Navigation Properties
        public virtual Comment Comment { get; set; } = null!;
        public virtual User Editor { get; set; } = null!;
    }
}
