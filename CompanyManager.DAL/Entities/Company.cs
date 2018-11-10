using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.DAL.Entities
{
    public class Company
    {
        public Company()
        {
            this.Employees = new List<Employee>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        [MinLength(5)]
        public string Headquarters { get; set; }
        
        public List<Employee> Employees { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
