namespace SalesControllerApi
{
    using Application.Models;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesAppService _employeesAppService;

        public EmployeesController(IEmployeesAppService employeesAppService)
        {
            _employeesAppService = employeesAppService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetList()
        {
            try
            {
                var employee = (EmployeeResponse)_employeesAppService.GetList().Result;
                if (!employee.Success)
                {
                    return NotFound(new { message = "No employee found." });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee.", error = ex.Message });
            }
        }
    }

}
