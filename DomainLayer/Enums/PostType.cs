using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum PostType
    {
        Original = 1,      // İlk post
        Reply = 2,         // Cevap
        Quote = 3,         // Alıntılı cevap
        Solution = 4,      // Çözüm önerisi
        Clarification = 5, // Açıklama
        Update = 6,        // Güncelleme
        Moderator = 7      // Moderatör notu
    }
}
