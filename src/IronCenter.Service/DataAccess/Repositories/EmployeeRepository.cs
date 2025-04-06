using IronCenter.Service.Data;
using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Employes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithSalaryAsync()
        {
            return await _context.Employees.Include(e => e.Salaries).ToListAsync();
        }
    }
}
