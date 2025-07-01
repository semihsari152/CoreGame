using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum GameListType
    {
        WantToPlay = 0,
        CurrentlyPlaying = 1,
        Completed = 2,
        OnHold = 3,
        Dropped = 4,
        Favorites = 5,
        Wishlist = 6,
        Owned = 7
    }
}
