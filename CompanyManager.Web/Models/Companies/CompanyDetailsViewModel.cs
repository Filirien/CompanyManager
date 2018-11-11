using CompanyManager.Web.Models.Employees;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Web.Models.Companies
{
    public class CompanyDetailsViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Headquarters { get; set; }

        public List<EmployeeViewModel> Employees { get; set; }
    }
}
