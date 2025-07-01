using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class GameTag : BaseEntity
    {
        public int GameId { get; set; }
        public int TagId { get; set; }

        // Navigation Properties
        public virtual Game Game { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
