using IronCenter.Service.Domain.Common;
using IronCenter.Service.Domain.Employers;

namespace IronCenter.Service.Domain.Employes
{
    public class Employee :Auditable
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty ;
        public int PositionId {  get; set; }
        public string PositionName { get; set; } = string.Empty;
        public decimal DailySalary { get; set; }
        public List<Salary> Salaries { get; set; }
    }
}
