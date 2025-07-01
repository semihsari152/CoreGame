using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.System
{
    public class NotificationAction : BaseEntity
    {
        public int NotificationId { get; set; }

        // Aksiyon Bilgileri
        public string ActionType { get; set; } = string.Empty; // "Accept", "Decline", "View", "Like"
        public string ActionText { get; set; } = string.Empty; // "Kabul Et", "Reddet"
        public string ActionUrl { get; set; } = string.Empty;
        public string? ActionIcon { get; set; }
        public string? ActionColor { get; set; } = "#007bff";

        // Durum
        public bool IsClicked { get; set; } = false;
        public DateTime? ClickedDate { get; set; }
        public bool IsEnabled { get; set; } = true;

        // Görünüm
        public int DisplayOrder { get; set; } = 0;
        public ActionStyle Style { get; set; } = ActionStyle.Primary;

        // Navigation Properties
        public virtual Notification Notification { get; set; } = null!;
    }
}
