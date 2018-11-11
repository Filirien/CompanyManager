using CompanyManager.Models.DTOs.Companies;

namespace CompanyManager.Models.Messages.Companies
{
    public class GetCompanyResponseMessage
    {
        public CompanyDTO Company { get; set; }
    }
}
