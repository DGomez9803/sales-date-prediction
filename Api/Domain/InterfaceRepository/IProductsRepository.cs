namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;

    public interface IProductsRepository
    {
        List<SelectItem> GetList();
    }
}
