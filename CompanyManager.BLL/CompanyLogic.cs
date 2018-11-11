using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyManager.BLL.Interfaces;
using CompanyManager.DAL;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.DTOs.Employees;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManager.BLL
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly CompanyDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyLogic(CompanyDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }


        public List<ListingCompanyDTO> AllCompanies()
            => dbContext.Companies
            .Where(c => c.IsActive)
            .ProjectTo<ListingCompanyDTO>(mapper.ConfigurationProvider)
            .ToList();

        public CompanyDTO AddCompany(AddCompanyDTO companyDto)
        {
            var company = mapper.Map<Company>(companyDto);

            dbContext.Add(company);
            dbContext.SaveChanges();

            var result = mapper.Map<CompanyDTO>(company);

            return result;
        }

        public CompanyDTO GetCompany(int id)
        {
            var company = dbContext.Companies
                .Include(c => c.Employees)
                .FirstOrDefault(c => c.Id == id && c.IsActive);

            var result = mapper.Map<CompanyDTO>(company);
            result.Employees = mapper.Map<List<EmployeeDTO>>(company.Employees.Where(x => x.IsActive));

            return result;

        }

        public CompanyDTO EditCompany(CompanyDTO companyDto)
        {
            var updatedCompany = mapper.Map<Company>(companyDto);

            var existingCompany = dbContext.Companies
                .FirstOrDefault(c => c.Id == companyDto.Id && c.IsActive);

            if (updatedCompany != null)
            {
                mapper.Map(updatedCompany, existingCompany);
                dbContext.SaveChanges();
            }

            var result = mapper.Map<CompanyDTO>(existingCompany);

            return result;
        }

        public void DeleteCompany(int id)
        {
            var company = dbContext.Companies
                .FirstOrDefault(c => c.Id == id && c.IsActive);

            if (company != null)
            {
                company.IsActive = false;
                dbContext.SaveChanges();
            }
        }
    }
}
