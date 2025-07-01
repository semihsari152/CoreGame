using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Forum
{
    public class TopicFollow : BaseEntity
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }

        // Bildirim Ayarları
        public bool NotifyOnNewPost { get; set; } = true;
        public bool NotifyOnBestAnswer { get; set; } = true;
        public bool NotifyOnStatusChange { get; set; } = false;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ForumTopic Topic { get; set; } = null!;
    }
}
