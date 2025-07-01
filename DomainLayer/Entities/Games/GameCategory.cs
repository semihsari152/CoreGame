using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class GameCategory : BaseEntity
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Game Game { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
