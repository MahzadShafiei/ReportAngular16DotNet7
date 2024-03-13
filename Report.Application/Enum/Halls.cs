using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("a")]        
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
    
    public static class EndStringAssumption
    {
        public const string Water = "";
        public const string Gas = "G_";
        public const string CompresAir = "FT_Tot";
        public const string Electricity= "EG_";
    }

    public static class EndStringPower
    {
        public const string Water = "";
        public const string Gas = "G_";
        public const string CompresAir = "FT";
        public const string Electricity = "PtotG_";
    }
}
