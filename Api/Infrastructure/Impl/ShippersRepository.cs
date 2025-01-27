namespace Infrastructure
{
    using Domain.Interfaces;
    using Domain.Models;
    using System.Collections.Generic;

    public class ShippersRepository : IShippersRepository
    {
        private readonly AppDbContext _dbContext;

        public ShippersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  List<SelectItem> GetList()
        {
            string query = "SELECT shipperid as Id , companyname as Value FROM Sales.Shippers";
            return _dbContext.ExecuteQueryAsync<SelectItem>(query).Result;
        }
    }

}
