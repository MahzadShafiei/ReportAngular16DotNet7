using System.ComponentModel.DataAnnotations;

namespace Report.Domain
{
    public class TagValue
    {
        [Key]
        public Guid MainId { get; set; }
        public int Id { get; set; }
        //public float value { get; set; }
        //public double value { get; set; }
        public Single value { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}
