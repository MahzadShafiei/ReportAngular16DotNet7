using Report.Application.Business;
using Report.Application.Dto.Include;
using Report.Domain;
using static Report.Application.Business.TagValueBusiness;

namespace Report.Application.Contract
{
    public interface ITagValueBusiness
    {
        Task<List<TagValue>> GetAll();
        Task<List<ChartModel>> GetGraphDataByFilter(FilterParameter filterParameter);
        Task<int> GetCalculatedAssumptionByFilter(FilterParameter filterParameter);
        Task<List<Management>> GetManagementByParameter(int parentId);
    }
}
