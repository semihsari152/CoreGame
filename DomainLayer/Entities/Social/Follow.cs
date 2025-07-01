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
    public class Follow : BaseEntity
    {
        public int FollowerId { get; set; }    // Takip eden
        public int FollowingId { get; set; }   // Takip edilen

        // Takip Türü
        public FollowType Type { get; set; } = FollowType.User;

        // Bildirim Ayarları
        public bool NotifyOnNewPost { get; set; } = true;
        public bool NotifyOnNewReview { get; set; } = true;
        public bool NotifyOnNewGuide { get; set; } = true;

        // Navigation Properties
        public virtual User Follower { get; set; } = null!;
        public virtual User Following { get; set; } = null!;
    }
}
