using Report.Domain;

namespace Report.Application.Contract
{
    public interface IEmployeeBusiness
    {
        Task<List<Employee>> GetAll();
    }
}
