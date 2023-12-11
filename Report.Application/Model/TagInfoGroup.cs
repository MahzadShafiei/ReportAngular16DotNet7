using Microsoft.VisualBasic;
using Report.Application.Enum;
using static Report.Application.Business.TagValueBusiness;

namespace Report.Application.Model
{
    public class TagInfoGroup
    {
        public int Key { get; set; }
        public int Count { get; set; }
        public List<int> DateGroup { get; set; }
    }
}
