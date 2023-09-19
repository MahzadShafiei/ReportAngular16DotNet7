using Report.Domain;

namespace Report.Application.Contract
{
    public interface ITagValueBusiness
    {
        Task<List<TagValue>> GetAll();
        Task<List<TagValue>> GetByFilter(string hallName, DateTime startDate, DateTime endDate, int meter);
    }
}
