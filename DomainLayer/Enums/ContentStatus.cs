using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ContentStatus
    {
        Draft = 0,
        PendingReview = 1,
        Published = 2,
        Archived = 3,
        Deleted = 4,
        Scheduled = 5,
        UnderReview = 6,
        Rejected = 7
    }
}
