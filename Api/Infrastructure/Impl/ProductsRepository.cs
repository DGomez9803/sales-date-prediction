namespace Infrastructure
{
    using Domain.Interfaces;
    using Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectItem> GetList()
        {
            string query = "SELECT productid as Id , productname as Value FROM Production.Products";
            return _dbContext.ExecuteQueryAsync<SelectItem>(query).Result;
        }
    }

}
