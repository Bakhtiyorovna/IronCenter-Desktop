using IronCenter.Service.Domain.Employes;
using IronCenter.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Domain.Employers
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }         
        public Employee Employee { get; set; }

        public DateTime GivenDate { get; set; }

        public Month Month { get; set; }

        public int Monthly { get; set; }
        public int TotalWorkDays { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
