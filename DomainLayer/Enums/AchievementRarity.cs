using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum AchievementRarity
    {
        Common = 0,     // %50+ kullanıcı
        Uncommon = 1,   // %25-50 kullanıcı
        Rare = 2,       // %10-25 kullanıcı
        Epic = 3,       // %5-10 kullanıcı
        Legendary = 4   // %5'ten az kullanıcı
    }
}
