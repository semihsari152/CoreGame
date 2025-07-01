using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class Platform : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // Steam, Epic, PlayStation, Xbox
        public string DisplayName { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        // Navigation Properties
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}
