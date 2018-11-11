using CompanyManager.Models.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManager.Web.Models.Companies
{
    public class EditCompanyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [MinLength(5)]
        public string Headquarters { get; set; }

        public List<EmployeeDTO> Employees { get; set; }
    }
}
