using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum PostStatus
    {
        Published = 1,
        Hidden = 2,
        Deleted = 3,
        UnderReview = 4,
        Approved = 5,
        Rejected = 6,
        Spam = 7
    }
}
