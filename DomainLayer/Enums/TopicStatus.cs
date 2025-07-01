using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum TopicStatus
    {
        Open = 1,
        Closed = 2,
        Locked = 3,
        Deleted = 4,
        Moved = 5,
        Merged = 6,
        UnderReview = 7,
        Resolved = 8,    // Soru cevaplanmış
        Archived = 9
    }
}
