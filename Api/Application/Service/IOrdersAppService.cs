namespace Application.Service
{
    using Application.Models;
    using Domain.Models;
    using System.Threading.Tasks;

    public interface IOrdersAppService
    {
        Task<OrderResponse> getOrdersByCustomers(string id);
        Task<OrderResponse> createOrder(OrderRequest request);
    }
}
