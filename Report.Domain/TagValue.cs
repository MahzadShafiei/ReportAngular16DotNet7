﻿namespace Report.Domain
{
    public class TagValue
    {
        public Guid Id { get; set; }
        public int TagInfoId { get; set; }
        //public float value { get; set; }
        //public double value { get; set; }
        public Single value { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}
