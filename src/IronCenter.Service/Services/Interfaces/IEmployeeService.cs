using IronCenter.Service.Domain.Employes;
using IronCenter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface IEmployeeService : IService<Employee>
    {
        Task<List<Employee>> GetEmployeesByPositionAsync(int positionId);
    }

}
