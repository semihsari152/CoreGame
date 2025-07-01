using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum PostEditType
    {
        ContentUpdate = 1,
        TypoFix = 2,
        Formatting = 3,
        AddInfo = 4,
        RemoveInfo = 5,
        Moderation = 6
    }
}
