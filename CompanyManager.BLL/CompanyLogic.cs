using AutoMapper;
using CompanyManager.BLL.Interfaces;
using CompanyManager.DAL;
using CompanyManager.DAL.Entities;

namespace CompanyManager.BLL
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly IMapper mapper;
        private readonly CompanyDbContext dbContext;

        public CompanyLogic(CompanyDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public Company AddCompany(Company company)
        {
            dbContext.Add(company);
            dbContext.SaveChanges();

            return company;
        }
    }
}
