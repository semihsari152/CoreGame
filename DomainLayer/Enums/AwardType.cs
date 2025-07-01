using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum AwardType
    {
        Quality = 1,      // Kaliteli review
        Helpful = 2,      // Yararlı review
        Detailed = 3,     // Detaylı review
        Funny = 4,        // Eğlenceli review
        FirstReview = 5,  // İlk review
        Popular = 6,      // Popüler review
        Expert = 7,       // Uzman görüşü
        Community = 8     // Topluluk seçimi
    }
}
