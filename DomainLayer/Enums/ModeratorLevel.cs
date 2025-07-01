using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ModeratorLevel
    {
        Helper = 1,        // Sadece yardım edebilir
        Moderator = 2,     // Standart moderatör
        SuperModerator = 3, // Gelişmiş yetkiler
        Admin = 4,         // Tam yetki
        Owner = 5          // Site sahibi
    }
}
