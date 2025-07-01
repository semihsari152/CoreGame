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
    public class ForumModerator : BaseEntity
    {
        public int UserId { get; set; }
        public int? CategoryId { get; set; } // null = global moderator

        // Yetki Seviyeleri
        public ModeratorLevel Level { get; set; }
        public string? Permissions { get; set; } // JSON array - specific permissions

        // Moderasyon İstatistikleri
        public int ActionsPerformed { get; set; } = 0;
        public DateTime? LastActionDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ForumCategory? Category { get; set; }
    }
}
