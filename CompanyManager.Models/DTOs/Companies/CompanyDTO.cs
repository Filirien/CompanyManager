using CompanyManager.Models.DTOs.Employees;
using System.Collections.Generic;

namespace CompanyManager.Models.DTOs.Companies
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Headquarters { get; set; }
        
        public List<EmployeeDTO> Employees { get; set; }
    }
}
