using AutoMapper;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.DTOs.Employees;

namespace CompanyManager.CompanyMicroservice.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCompanyDTO, Company>();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Company,ListingCompanyDTO>();
            CreateMap<AddEmployeeDTO, Employee>();
        }
    }
}
