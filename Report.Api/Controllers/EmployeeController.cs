using Microsoft.AspNetCore.Mvc;
using Report.Application.Contract;

namespace Report.Api.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBusiness employeeBusiness;

        public EmployeeController( IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            var result= employeeBusiness.GetAll();
            return Ok(result);
        }
                
    }
}
