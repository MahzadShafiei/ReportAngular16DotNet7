﻿using System.ComponentModel.DataAnnotations;

namespace Report.Domain
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
