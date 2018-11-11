using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyManager.BLL.Interfaces;
using CompanyManager.DAL;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Employees;
using System.Linq;

namespace CompanyManager.BLL
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly CompanyDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeLogic(CompanyDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public EmployeeDTO GetEmployee(int id)
            => dbContext.Employees
                .Where(c => c.Id == id && c.IsActive)
                .ProjectTo<EmployeeDTO>(mapper.ConfigurationProvider)
                .FirstOrDefault( );

        public void AddEmployee(int companyId, AddEmployeeDTO employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            var company = dbContext.Companies
                .FirstOrDefault(c => c.Id == companyId && c.IsActive);

            if (company != null)
            {
                company.Employees.Add(employee);
                dbContext.SaveChanges();
            }
        }

        public EmployeeDTO EditEmployee(EmployeeDTO employeeDto)
        {
            var updatedEmployee = mapper.Map<Employee>(employeeDto);

            var existingEmployee = dbContext.Employees
                .FirstOrDefault(c => c.Id == employeeDto.Id && c.IsActive);

            if (updatedEmployee != null)
            {
                mapper.Map(updatedEmployee, existingEmployee);
                dbContext.SaveChanges();
            }

            var result = mapper.Map<EmployeeDTO>(existingEmployee);

            return result;
        }

        public void DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.FirstOrDefault(c => c.Id == id && c.IsActive);

            if (employee != null)
            {
                employee.IsActive = false;
                dbContext.SaveChanges();
            }
        }
    }
}
