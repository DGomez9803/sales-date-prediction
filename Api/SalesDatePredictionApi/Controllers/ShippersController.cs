namespace SalesControllerApi
{
    using Application.Models;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShippersController : ControllerBase
    {
        private readonly IShippersAppService _shippersAppService;

        public ShippersController(IShippersAppService shippersAppService)
        {
            _shippersAppService = shippersAppService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShipperResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetList()
        {
            try
            {
                var shippers = (ShipperResponse)_shippersAppService.GetList().Result;
                if (!shippers.Success)
                {
                    return NotFound(new { message = "No shippers found." });
                }

                return Ok(shippers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving shippers.", error = ex.Message });
            }
        }
    }

}
