using AutoMapper;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Web.Models.Companies;

namespace CompanyManager.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCompanyViewModel, AddCompanyDTO>();
        }
    }
}
