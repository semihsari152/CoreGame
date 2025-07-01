using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.System
{
    public class NotificationQueue : BaseEntity
    {
        public int NotificationId { get; set; }
        public NotificationChannel Channel { get; set; }

        // Queue Durumu
        public QueueStatus Status { get; set; } = QueueStatus.Pending;
        public int AttemptCount { get; set; } = 0;
        public int MaxAttempts { get; set; } = 3;
        public DateTime? NextAttemptDate { get; set; }
        public DateTime? ProcessedDate { get; set; }

        // Hata Bilgileri
        public string? ErrorMessage { get; set; }
        public string? ErrorDetails { get; set; }

        // Priority ve Scheduling
        public int Priority { get; set; } = 5; // 1-10 arası, 1 en yüksek
        public DateTime ScheduledDate { get; set; }

        // Navigation Properties
        public virtual Notification Notification { get; set; } = null!;
    }
}
