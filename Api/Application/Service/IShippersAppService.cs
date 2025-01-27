namespace Application.Service
{
    using Application.Models;
    using System.Threading.Tasks;

    public interface IShippersAppService
    {
        Task<ShipperResponse> GetList();
    }
}
