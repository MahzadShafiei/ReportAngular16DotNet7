using Microsoft.VisualBasic;
using Report.Application.Enum;
using static Report.Application.Business.TagValueBusiness;

namespace Report.Application.Model
{
    public class TimeStampHourGroup
    {
        public int Key { get; set; }
        public int Count { get; set; }
        public Single MainValue { get; set; }
        public double Average { get; set; }
    }
}
