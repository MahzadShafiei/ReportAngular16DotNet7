namespace Report.Application.Dto.Include
{
    public class FilterParameter
    {        
        public string HallName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Meter { get; set; }     
        public string Period { get; set; }
    }
}
