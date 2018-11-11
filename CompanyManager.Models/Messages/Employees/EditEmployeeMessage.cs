using CompanyManager.Models.DTOs.Employees;

namespace CompanyManager.Models.Messages.Employees
{
    public class EditEmployeeMessage
    {
        public EmployeeDTO Employee { get; set; }
    }
}
