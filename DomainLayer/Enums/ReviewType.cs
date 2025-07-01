using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum ReviewType
    {
        Quick = 1,        // Hızlı review (kısa)
        Full = 2,         // Detaylı review
        Video = 3,        // Video review
        Stream = 4,       // Canlı yayın review
        Professional = 5,  // Profesyonel review
        FirstImpression = 6, // İlk izlenim
        AfterPatch = 7    // Güncelleme sonrası
    }
}
