using IronCenter.Service.Domain.Employes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface IEmployeeService 
    {
        Task<List<Employee>> GetEmployeesByPositionAsync(int positionId);
    }

}
