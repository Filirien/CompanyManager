using AutoMapper;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.DTOs.Employees;
using CompanyManager.Web.Models.Companies;
using CompanyManager.Web.Models.Employees;

namespace CompanyManager.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCompanyViewModel, AddCompanyDTO>();
            CreateMap<CompanyDetailsViewModel, CompanyDTO>().ReverseMap();
            CreateMap<EditCompanyViewModel, CompanyDTO>();
            CreateMap<AddEmployeeViewModel, AddEmployeeDTO>();
            CreateMap<EmployeeDTO, EmployeeViewModel>();
        }
    }
}
