namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;

    public interface ICustomerRepository
    {
        List<Customer> GetList(string companyName);
    }
}
