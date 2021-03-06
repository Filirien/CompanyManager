﻿using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Web.Models.Companies
{
    public class AddCompanyViewModel
    {
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [MinLength(5)]
        public string Headquarters { get; set; }
    }
}
