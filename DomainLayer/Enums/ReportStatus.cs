using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ReportStatus
    {
        Pending = 0,
        UnderReview = 1,
        Resolved = 2,
        Dismissed = 3,
        Escalated = 4
    }
}
