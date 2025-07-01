using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum CompletionStatus
    {
        NotStarted = 0,
        InProgress = 1,
        MainStoryCompleted = 2,
        FullyCompleted = 3,    // %100 tamamlama
        Abandoned = 4,
        EndlessGame = 5        // Sonsuz oyun (MMO vs.)
    }
}
