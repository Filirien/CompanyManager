using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.DTOs.Employees;
using System.Collections.Generic;

namespace CompanyManager.BLL.Interfaces
{
    public interface ICompanyLogic
    {
        CompanyDTO AddCompany(AddCompanyDTO companyDto);

        CompanyDTO GetCompany(int id);

        CompanyDTO EditCompany(CompanyDTO companyDto);

        List<ListingCompanyDTO> AllCompanies();

        void DeleteCompany(int id);
    }
}
