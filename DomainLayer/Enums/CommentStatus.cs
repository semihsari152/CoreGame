using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum CommentStatus
    {
        Draft = 0,
        Published = 1,
        Hidden = 2,
        Deleted = 3,
        Reported = 4,
        UnderReview = 5,
        Approved = 6,
        Rejected = 7
    }
}
