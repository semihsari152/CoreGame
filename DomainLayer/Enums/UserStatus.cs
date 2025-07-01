using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum UserStatus
    {
        Active = 0,
        Inactive = 1,
        Suspended = 2,
        Banned = 3,
        PendingVerification = 4
    }
}
