namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;

    public interface IShippersRepository
    {
       List<SelectItem> GetList();
    }
}
