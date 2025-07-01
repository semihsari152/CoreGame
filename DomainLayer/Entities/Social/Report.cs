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
    public class Report : BaseEntity
    {
        public int ReporterId { get; set; }

        // Neyin report edildiği
        public ReportableType ReportableType { get; set; }
        public int ReportableId { get; set; }

        // Report Detayları
        public ReportReason Reason { get; set; }
        public string? Description { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        // Moderasyon
        public int? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ModeratorNotes { get; set; }
        public ReportAction? ActionTaken { get; set; }

        // Navigation Properties
        public virtual User Reporter { get; set; } = null!;
        public virtual User? Reviewer { get; set; }
    }
}
