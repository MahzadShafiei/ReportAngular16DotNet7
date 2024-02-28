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
        [Route("GetByFilter")]
        public async Task<IActionResult> GetByFilter([FromQuery]FilterParameter filterParameter)
        {
            var result = await tagValueBusiness.GetByFilter(filterParameter);
            return Ok(result);            
        }

        [HttpGet]
        [Route("GetCalculatedAssumption")]
        public async Task<IActionResult> GetCalculatedAssumption([FromQuery] FilterParameter filterParameter)
        {
            var result= await tagValueBusiness.GetCalculatedAssumption(filterParameter);
            return Ok(result);
        }
    }
}
