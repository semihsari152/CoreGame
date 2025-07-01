using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.System
{
    public class NotificationTemplateAction : BaseEntity
    {
        public int TemplateId { get; set; }

        public string ActionType { get; set; } = string.Empty;
        public string ActionTextTemplate { get; set; } = string.Empty;
        public string ActionUrlTemplate { get; set; } = string.Empty;
        public string? ActionIcon { get; set; }
        public string? ActionColor { get; set; }
        public ActionStyle Style { get; set; } = ActionStyle.Primary;
        public int DisplayOrder { get; set; } = 0;
        public bool IsEnabled { get; set; } = true;

        // Navigation Properties
        public virtual NotificationTemplate Template { get; set; } = null!;
    }
}
