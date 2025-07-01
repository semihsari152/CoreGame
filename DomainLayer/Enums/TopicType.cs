using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum TopicType
    {
        Discussion = 1,    // Genel tartışma
        Question = 2,      // Soru
        Guide = 3,         // Rehber
        News = 4,          // Haber
        Announcement = 5,  // Duyuru
        Poll = 6,          // Anket
        Bug = 7,           // Hata raporu
        Feature = 8,       // Özellik önerisi
        Review = 9,        // İnceleme
        Showcase = 10      // Sergileme (screenshot vs.)
    }
}
