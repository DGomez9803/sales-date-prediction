namespace Application.Service
{
    using Domain.Interfaces;
    using Domain.Models;
    using Infrastructure;
    using System.Collections.Generic;

    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _dbContext;

        public OrdersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool createOrder(OrderRequest request)
        {
            string query = "InsertOrder";
            var parameters = new Dictionary<string, object>
            {
                { "@CustId", request.CustId },
                { "@EmpId", request.EmpId },
                { "@ShipperId", request.ShipperId },
                { "@ShipName", request.ShipName },
                { "@ShipAddress", request.ShipAddress },
                { "@ShipCity", request.ShipCity },
                { "@ShipCountry", request.ShipCountry },
                { "@OrderDate", request.OrderDate },
                { "@RequiredDate", request.RequiredDate },
                { "@ShippedDate", request.ShippedDate },
                { "@Freight", request.Freight },
                { "@ProductId", request.ProductId },
                { "@UnitPrice", request.Freight },
                { "@Quantity", request.Quantity },
                { "@Discount", request.Discount },
            };
            try
            {
                _ = _dbContext.ExecuteStoredProcedureAsync<object>(query, parameters);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public List<Order> getOrdersByCustomers(string id)
        {
            string query = "SELECT orderid, requireddate, shippeddate, shipname, shipaddress, shipcity, shipcountry FROM Sales.Orders WHERE custid = " + id;
            return _dbContext.ExecuteQueryAsync<Order>(query).Result;
        }
    }

}
