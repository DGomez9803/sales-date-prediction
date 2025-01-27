namespace Application.Service
{
    using Application.Models;
    using Domain.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepositor;

        public CustomerAppService(ICustomerRepository customerRepositor)
        {
            _customerRepositor = customerRepositor;
        }

        public Task<CustomerResponse> GetList(string companyName)
        {
            var data = this._customerRepositor.GetList(companyName);

            if (data == null || data.Count == 0 || !data.Any())
            {
                return Task.FromResult(
                    new CustomerResponse
                    {
                        Success = false,
                        Error = "No data found."
                    }
                );
            }

            return Task.FromResult(
                new CustomerResponse
                {
                    Success = true,
                    Data = data
                }
            );
        }
    }

}
