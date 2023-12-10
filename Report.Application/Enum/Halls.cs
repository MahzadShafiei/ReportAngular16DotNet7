using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Enum
{
    public class Halls
    {
    }
    public enum HallType
    {
        Paint = 1,
        Body = 2
    }

    public enum Meter
    {
        Water = 1,
        Gas = 2,
        CompresAir = 4,
        Electricity = 5,
    }

    public enum Period
    {
        Minute = 0,
        Hour = 1,
        Day = 2,
        Month = 3,
    }
}
