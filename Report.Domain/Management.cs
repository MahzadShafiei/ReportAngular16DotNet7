using System.ComponentModel.DataAnnotations;

namespace Report.Domain
{
    public class Management
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
