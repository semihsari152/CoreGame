using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum GamePlayStatus
    {
        WantToPlay = 0,
        Playing = 1,
        Completed = 2,
        OnHold = 3,
        Dropped = 4,
        Replay = 5
    }
}
