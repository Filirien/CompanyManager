using CompanyManager.Models.DTOs.Employees;

namespace CompanyManager.Models.Messages.Employees
{
    public class AddEmployeeMessage
    {
        public int CompanyId { get; set; }

        public AddEmployeeDTO Employee { get; set; }
    }
}
