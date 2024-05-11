namespace Report.Domain
{
    public class Formula
    {
        public int Id { get; set; }
        public string PersianName { get; set; }
        public int HallCode { get; set; }
        public int HallType { get; set; }
        public int Meter { get; set; }
        public string SensorCode { get; set; }
        public int UsagePercent { get; set; }
        public int ManagementId { get; set; }
    }
}
