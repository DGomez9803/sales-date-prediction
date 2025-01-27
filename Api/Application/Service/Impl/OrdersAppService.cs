namespace Application.Service
{
    using Application.Models;
    using Domain.Interfaces;
    using Domain.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersAppService : IOrdersAppService
    {
        private readonly IOrdersRepository _ordersRepositor;

        public OrdersAppService(IOrdersRepository ordersRepositor)
        {
            _ordersRepositor = ordersRepositor;
        }

        public Task<OrderResponse> createOrder(OrderRequest request)
        {
            var data = this._ordersRepositor.createOrder(request);

            if (data)
            {
                return Task.FromResult(
                    new OrderResponse
                    {
                        Success = true,
                    }
                );
            }
            else
            {
                return Task.FromResult(
                    new OrderResponse
                    {
                        Success = false,
                        Error = "No save."
                    }
                );
            }
        }


        public Task<OrderResponse> getOrdersByCustomers(string id)
        { 
            var data = this._ordersRepositor.getOrdersByCustomers(id);

            if (data == null || data.Count == 0 || !data.Any())
            {
                return Task.FromResult(
                    new OrderResponse
                    {
                        Success = false,
                        Error = "No data found."
                    }
                );
            }

            return Task.FromResult(
                new OrderResponse
                {
                    Success = true,
                    Data = data
                }
            );
        }
    }

}
