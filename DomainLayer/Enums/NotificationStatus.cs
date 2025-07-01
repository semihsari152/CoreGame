using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum NotificationStatus
    {
        NotSent = 0,
        Pending = 1,
        Sent = 2,
        Delivered = 3,
        Failed = 4,
        Bounced = 5,
        Opened = 6,
        Clicked = 7
    }
}
