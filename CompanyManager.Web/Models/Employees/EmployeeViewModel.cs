using CompanyManager.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.Web.Models.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Experience level")]
        public ExperienceLevel ExperienceLevel { get; set; }

        [Display(Name = "Starting Date")]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Display(Name = "Vacation days")]
        public int VacationDays { get; set; }
    }
}
