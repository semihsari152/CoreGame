using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ReviewEditType
    {
        ContentUpdate = 1,
        RatingChange = 2,
        TypoFix = 3,
        AdditionalInfo = 4,
        Clarification = 5,
        Moderation = 6
    }
}
