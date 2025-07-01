using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class GamePlatform : BaseEntity
    {
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public string? StoreUrl { get; set; }
        public decimal? PlatformPrice { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Navigation Properties
        public virtual Game Game { get; set; } = null!;
        public virtual Platform Platform { get; set; } = null!;
    }
}
