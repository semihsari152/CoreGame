using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Users
{
    public class UserAchievement : BaseEntity
    {
        public int UserId { get; set; }
        public int AchievementId { get; set; }

        // Achievement Bilgileri
        public DateTime EarnedDate { get; set; }
        public int PointsEarned { get; set; } = 0;
        public bool IsDisplayed { get; set; } = true;
        public string? EarnedDescription { get; set; } // Achievement kazanıldığında açıklama

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Achievement Achievement { get; set; } = null!;
    }
}
