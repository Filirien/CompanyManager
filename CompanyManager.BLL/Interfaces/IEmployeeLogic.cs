using CompanyManager.Models.DTOs.Employees;

namespace CompanyManager.BLL.Interfaces
{
    public interface IEmployeeLogic
    {
        EmployeeDTO GetEmployee(int id);

        void AddEmployee(int companyId, AddEmployeeDTO employeeDto);

        EmployeeDTO EditEmployee(EmployeeDTO employeeDto);

        void DeleteEmployee(int id);
    }
}
