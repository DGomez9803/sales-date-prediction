namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;

    public interface IEmployeesRepository
    {
       List<SelectItem> GetList();
    }
}
