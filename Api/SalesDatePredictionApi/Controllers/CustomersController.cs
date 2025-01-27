namespace SalesControllerApi
{
    using Application.Models;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomersController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet("GetList/{companyName?}")]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetList(string? companyName)
        {
            companyName ??= "";
            try
            {
                var customers = (CustomerResponse) _customerAppService.GetList(companyName).Result;
                if (!customers.Success)
                {
                    return NotFound(new { message = "No customers found." });
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving customers.", error = ex.Message });
            }
        }

    }

}
