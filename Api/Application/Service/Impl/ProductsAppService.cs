namespace Application.Service
{
    using Application.Models;
    using Domain.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsAppService : IProductsAppService
    {
        private readonly IProductsRepository _productsRepositor;

        public ProductsAppService(IProductsRepository productsRepositor)
        {
            _productsRepositor = productsRepositor;
        }

        public Task<ProductResponse> GetList()
        {
            var data = this._productsRepositor.GetList();

            if (data == null || data.Count == 0 || !data.Any())
            {
                return Task.FromResult(
                    new ProductResponse
                    {
                        Success = false,
                        Error = "No data found."
                    }
                );
            }

            return Task.FromResult(
                new ProductResponse
                {
                    Success = true,
                    Data = data
                }
            );
        }
    }

}
