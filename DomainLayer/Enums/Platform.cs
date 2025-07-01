using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    [Flags]
    public enum Platform
    {
        None = 0,
        Windows = 1,
        MacOS = 2,
        Linux = 4,
        PlayStation4 = 8,
        PlayStation5 = 16,
        Xbox = 32,
        XboxSeriesX = 64,
        NintendoSwitch = 128,
        iOS = 256,
        Android = 512,
        Web = 1024
    }
}
