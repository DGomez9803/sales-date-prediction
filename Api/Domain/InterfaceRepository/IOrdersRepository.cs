namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;

    public interface IOrdersRepository
    {
        List<Order> getOrdersByCustomers(string id);
        bool createOrder(OrderRequest request);
    }
}
