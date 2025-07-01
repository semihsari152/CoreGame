using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ReportAction
    {
        NoAction = 0,
        Warning = 1,
        ContentRemoved = 2,
        ContentEdited = 3,
        UserSuspended = 4,
        UserBanned = 5,
        ContentMoved = 6
    }
}
