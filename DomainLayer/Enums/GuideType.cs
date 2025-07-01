using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum GuideType
    {
        Walkthrough = 1,    // Tam çözüm
        Tutorial = 2,       // Öğretici
        TipsAndTricks = 3,  // İpuçları
        Strategy = 4,       // Strateji
        Build = 5,          // Karakter/Deck build
        Speedrun = 6,       // Hızlı tamamlama
        Achievement = 7,    // Achievement rehberi
        Troubleshooting = 8, // Sorun giderme
        Modding = 9,        // Mod kurulumu
        Beginner = 10       // Yeni başlayanlar
    }
}
