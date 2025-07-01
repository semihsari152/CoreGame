using DomainLayer.Common;
using DomainLayer.Entities.Games;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Users
{
    public class UserGameList : BaseEntity
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public GameListType ListType { get; set; }

        // Oyun Durumu ve İstatistikler
        public GamePlayStatus PlayStatus { get; set; } = GamePlayStatus.WantToPlay;
        public decimal? PersonalRating { get; set; } // 1-10 arası
        public int HoursPlayed { get; set; } = 0;
        public DateTime? StartedPlayingDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? LastPlayedDate { get; set; }

        // Kişisel Notlar
        public string? PersonalNotes { get; set; }
        public string? PersonalTags { get; set; } // Virgülle ayrılmış
        public bool IsFavorite { get; set; } = false;
        public bool IsPrivate { get; set; } = false;

        // İlerleme
        public int CompletionPercentage { get; set; } = 0;
        public string? CurrentLevel { get; set; }
        public string? CurrentObjective { get; set; }

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Game Game { get; set; } = null!;
    }
}
