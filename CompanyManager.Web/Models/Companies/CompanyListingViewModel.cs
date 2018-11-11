using CompanyManager.Models.DTOs.Employees;
using System.Collections.Generic;

namespace CompanyManager.Web.Models.Companies
{
    public class CompanyListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Headquarters { get; set; }
    }
}
