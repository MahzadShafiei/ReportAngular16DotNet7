using Microsoft.VisualBasic;
using Report.Application.Enum;
using Report.Domain;

namespace Report.Application.Dto.Include
{
    
    public class FilterParameter
    {
        public int? AssisttanceType { get; set; }
        public int? ManagementType { get; set; }
        public int? HallType { get; set; }
        public string HallCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Meter Meter { get; set; }     
        public Period Period { get; set; }
    }
}
