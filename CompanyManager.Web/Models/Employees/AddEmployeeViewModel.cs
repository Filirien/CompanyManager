using CompanyManager.Models.Enums;
using System;

namespace CompanyManager.Web.Models.Employees
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public ExperienceLevel ExperienceLevel { get; set; }
        
        public DateTime StartingDate { get; set; }
        
        public decimal Salary { get; set; }
        
        public int VacationDays { get; set; }

        public int CompanyId { get; set; }
    }
}
