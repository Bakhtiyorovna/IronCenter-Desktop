using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Employes;
using IronCenter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> GetEmployeesByPositionAsync(int positionId)
        {
            var allEmployees = await _repository.GetAllAsync();
            return allEmployees.Where(e => e.PositionId == positionId).ToList();
        }
    }

}
