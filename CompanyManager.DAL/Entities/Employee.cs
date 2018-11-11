using CompanyManager.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        public ExperienceLevel ExperienceLevel { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime StartingDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        
        public int VacationDays { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
