namespace Application.Service
{
    using Application.Models;
    using Domain.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeesAppService : IEmployeesAppService
    {
        private readonly IEmployeesRepository _employeesRepositor;

        public EmployeesAppService(IEmployeesRepository employeesRepositor)
        {
            _employeesRepositor = employeesRepositor;
        }

        public Task<EmployeeResponse> GetList()
        {
            var data = this._employeesRepositor.GetList();

            if (data == null || data.Count == 0 || !data.Any())
            {
                return Task.FromResult(
                    new EmployeeResponse
                    {
                        Success = false,
                        Error = "No data found."
                    }
                );
            }

            return Task.FromResult(
                new EmployeeResponse
                {
                    Success = true,
                    Data = data
                }
            );
        }
    }

}
