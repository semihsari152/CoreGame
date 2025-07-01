using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class GuideBookmark : BaseEntity
    {
        public int GuideId { get; set; }
        public int UserId { get; set; }
        public string? Notes { get; set; }
        public string? FolderName { get; set; } // Kullanıcı klasörleri

        // Navigation Properties
        public virtual Guide Guide { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
