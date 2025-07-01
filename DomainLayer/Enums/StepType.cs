using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum StepType
    {
        Action = 1,      // Yapılacak işlem
        Information = 2, // Bilgi verme
        Warning = 3,     // Uyarı
        Tip = 4,         // İpucu
        Note = 5,        // Not
        Decision = 6,    // Karar verme noktası
        Checkpoint = 7   // Kontrol noktası
    }
}
