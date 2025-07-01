using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ReviewStatus
    {
        Draft = 0,
        Published = 1,
        UnderReview = 2,
        Rejected = 3,
        Hidden = 4,
        Deleted = 5,
        Featured = 6,
        Archived = 7
    }
}
