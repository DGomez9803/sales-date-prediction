namespace SalesControllerApi
{
    using Application.Models;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsAppService _productsAppService;

        public ProductsController(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetList()
        {
            try
            {
                var products = (ProductResponse)_productsAppService.GetList().Result;
                if (!products.Success)
                {
                    return NotFound(new { message = "No products found." });
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products.", error = ex.Message });
            }
        }
    }

}
