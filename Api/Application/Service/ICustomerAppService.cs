namespace Application.Service
{
    using Application.Models;
    using System.Threading.Tasks;

    public interface ICustomerAppService
    {
        Task<CustomerResponse> GetList(string companyName);
    }
}
