using Microsoft.AspNetCore.Mvc;
using Report.Application.Business;
using Report.Application.Contract;
using Report.Application.Dto.Include;

namespace Report.Api.Controllers
{
    [ApiController]
    [Route("api/TagValue")]
    public class TagValueController : Controller
    {
        private readonly ITagValueBusiness tagValueBusiness;

        public TagValueController(ITagValueBusiness tagValueBusiness)
        {
            this.tagValueBusiness = tagValueBusiness;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await tagValueBusiness.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetGraphDataByFilter")]
        public async Task<IActionResult> GetGraphDataByFilter([FromQuery] FilterParameter filterParameter)
        {
            var result = await tagValueBusiness.GetGraphDataByFilter(filterParameter);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCalculatedAssumptionByFilter")]
        public async Task<IActionResult> GetCalculatedAssumptionByFilter([FromQuery] FilterParameter filterParameter)
        {
            var result = await tagValueBusiness.GetCalculatedAssumptionByFilter(filterParameter);
            return Ok(result);
        }

        [HttpGet("GetManagementByParameter")]
        public async Task<IActionResult> GetManagementByParameter(int parentId)
        {
            var result = await tagValueBusiness.GetManagementByParameter(parentId);
            return Ok(result);
        }
    }
}
