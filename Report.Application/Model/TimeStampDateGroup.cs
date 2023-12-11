using Microsoft.VisualBasic;
using Report.Application.Dto.Include;
using Report.Application.Enum;
using static Report.Application.Business.TagValueBusiness;

namespace Report.Application.Model
{
    public class TimeStampDateGroup
    {
        public DateTime Key { get; set; }
        public string PersianDate { get; set; }
        public int Count { get; set; }
        public List<ChartModel> HourGroup { get; set; }
    }
}
