namespace SalesControllerApi
{
    using Application.Models;
    using Application.Service;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersAppService _ordersAppService;

        public OrdersController(IOrdersAppService ordersAppService)
        {
            _ordersAppService = ordersAppService;
        }

        [HttpGet("getOrdersByCustomers/{customerId}")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult getOrdersByCustomers(string customerId)
        {
            try
            {
                var orders = (OrderResponse)_ordersAppService.getOrdersByCustomers(customerId).Result;
                if (!orders.Success)
                {
                    return NotFound(new { message = "No order found by "+ customerId });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving orders.", error = ex.Message });
            }
         
        }


        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(500)]
        public IActionResult createOrder([FromBody] OrderRequest request)
        {
            try
            {
                var orders = (OrderResponse)_ordersAppService.createOrder(request).Result;
                if (!orders.Success)
                {
                    return NotFound(new { message = "No sabe order"});
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while save order.", error = ex.Message });
            }
        }
    }

}
