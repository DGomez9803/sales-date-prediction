namespace Application.Service
{
    using Application.Models;
    using System.Threading.Tasks;

    public interface IEmployeesAppService
    {
        Task<EmployeeResponse> GetList();
    }
}
