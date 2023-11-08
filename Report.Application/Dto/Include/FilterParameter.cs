using Microsoft.VisualBasic;
using Report.Application.Enum;

namespace Report.Application.Dto.Include
{
    
    public class FilterParameter
    {        
        public HallType HallType { get; set; }
        public string HallCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Meter Meter { get; set; }     
        public Period Period { get; set; }
    }
}
