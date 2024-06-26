﻿using Microsoft.EntityFrameworkCore;
using Report.Domain;

namespace Report.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<TagValue> TagValue { get; set; }
        public DbSet<TagInfo> TagInfo { get; set; }
        public DbSet<Formula> Formula { get; set; }
        public DbSet<Unit> Unit { get; set; }
    }
}
