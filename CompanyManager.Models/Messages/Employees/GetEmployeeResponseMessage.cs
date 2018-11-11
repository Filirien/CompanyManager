using CompanyManager.Models.DTOs.Employees;

namespace CompanyManager.Models.Messages.Employees
{
    public class GetEmployeeResponseMessage
    {
        public EmployeeDTO Employee { get; set; }
    }
}
