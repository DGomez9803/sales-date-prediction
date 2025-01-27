namespace Application.Service
{
    using Application.Models;
    using Domain.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShippersAppService : IShippersAppService
    {
        private readonly IShippersRepository _shippersRepositor;

        public ShippersAppService(IShippersRepository shippersRepositor)
        {
            _shippersRepositor = shippersRepositor;
        }

        public Task<ShipperResponse> GetList()
        {
            var data = this._shippersRepositor.GetList();

            if (data == null || data.Count == 0 || !data.Any())
            {
                return Task.FromResult(
                    new ShipperResponse
                    {
                        Success = false,
                        Error = "No data found."
                    }
                );
            }

            return Task.FromResult(
                new ShipperResponse
                {
                    Success = true,
                    Data = data
                }
            );
        }
    }

}
