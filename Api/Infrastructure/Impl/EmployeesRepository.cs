namespace Infrastructure
{
    using Domain.Interfaces;
    using Domain.Models;
    using System.Collections.Generic;

    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectItem> GetList()
        {
            string query = "SELECT empid as Id , CONCAT(titleofcourtesy, ' ', firstname, ' ', lastname) as Value FROM HR.Employees\r\n";
            return  _dbContext.ExecuteQueryAsync<SelectItem>(query).Result;
        }
    }

}
