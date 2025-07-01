using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class GuideStep : BaseEntity
    {
        public int GuideId { get; set; }

        // Step İçeriği
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? TipText { get; set; }
        public string? WarningText { get; set; }

        // Sıralama ve Hiyerarşi
        public int StepNumber { get; set; }
        public int? ParentStepId { get; set; }
        public int Level { get; set; } = 0; // 0 = ana adım, 1+ = alt adımlar

        // Medya ve Görsel
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }

        // Step Özellikleri
        public StepType Type { get; set; } = StepType.Action;
        public bool IsOptional { get; set; } = false;
        public bool IsImportant { get; set; } = false;
        public int? EstimatedTime { get; set; } // Bu adım için tahmini süre

        // Koşullar ve Dallanma
        public string? Conditions { get; set; } // JSON - bu adımın çalışma koşulları
        public string? AlternativeSteps { get; set; } // JSON - alternatif adımlar

        // Navigation Properties
        public virtual Guide Guide { get; set; } = null!;
        public virtual GuideStep? ParentStep { get; set; }
        public virtual ICollection<GuideStep> SubSteps { get; set; } = new List<GuideStep>();
        public virtual ICollection<Media> StepMedia { get; set; } = new List<Media>();
    }
}
