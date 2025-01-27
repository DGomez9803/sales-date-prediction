namespace Application.Service
{
    using Application.Models;
    using System.Threading.Tasks;

    public interface IProductsAppService
    {
        Task<ProductResponse> GetList();
    }
}
