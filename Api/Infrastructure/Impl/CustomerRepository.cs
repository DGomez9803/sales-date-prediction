namespace Infrastructure
{
    using Azure.Core;
    using Domain.Interfaces;
    using Domain.Models;
    using System.Collections.Generic;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> GetList(string companyName)
        {
            string query = "GetAllCustomersNextPredictedOrderDate";
            var parameters = new Dictionary<string, object>
            {
                { "@CompanyName", companyName },
            };
            return _dbContext.ExecuteStoredProcedureAsync<Customer>(query,parameters).Result;
        }
    }

}
