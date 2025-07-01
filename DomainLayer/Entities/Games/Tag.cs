using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ColorCode { get; set; } = "#007bff";
        public bool IsActive { get; set; } = true;
        public int UsageCount { get; set; } = 0;

        // Navigation Properties
        public virtual ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();
    }
}
