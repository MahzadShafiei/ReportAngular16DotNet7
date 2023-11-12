using Report.Application.Dto.Include;
using Report.Domain;

namespace Report.Application.Contract
{
    public interface ITagValueBusiness
    {
        Task<List<TagValue>> GetAll();
        Task<List<ChartModel>> GetByFilter(FilterParameter filterParameter);
    }
}
