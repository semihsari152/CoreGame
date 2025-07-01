using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum MediaStatus
    {
        Active = 1,
        Inactive = 2,
        Processing = 3,
        Failed = 4,
        Deleted = 5,
        Quarantined = 6
    }
}
