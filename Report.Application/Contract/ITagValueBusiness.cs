using Report.Application.Business;
using Report.Application.Dto.Include;
using Report.Domain;
using static Report.Application.Business.TagValueBusiness;

namespace Report.Application.Contract
{
    public interface ITagValueBusiness
    {
        Task<List<TagValue>> GetAll();
        Task<List<ChartModel>> GetByFilter(FilterParameter filterParameter);
        Task<int> GetCalculatedAssumption(FilterParameter filterParameter);
    }
}
