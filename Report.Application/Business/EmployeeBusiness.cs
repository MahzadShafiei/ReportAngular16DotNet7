using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
using Report.Domain;
using Report.Persistance;

namespace Report.Application.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly DataContext dataContext;

        public EmployeeBusiness(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await dataContext.Employees.ToListAsync();
             
        }
    }
}
