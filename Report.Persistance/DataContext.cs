using Microsoft.EntityFrameworkCore;
using Report.Domain;

namespace Report.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
