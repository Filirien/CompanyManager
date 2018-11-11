using CompanyManager.Models.Enums;
using System;

namespace CompanyManager.Models.DTOs.Employees
{
    public class AddEmployeeDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }
    }
}
