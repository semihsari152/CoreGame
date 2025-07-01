using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum MessageType
    {
        Private = 1,
        System = 2,
        Announcement = 3,
        Warning = 4,
        Welcome = 5
    }
}
