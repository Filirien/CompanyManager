using AutoMapper;
using CompanyManager.BLL;
using CompanyManager.CompanyMicroservice;
using CompanyManager.CompanyMicroservice.Infrastructure.Mapping;
using CompanyManager.DAL;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.DTOs.Employees;
using CompanyManager.Web.Models.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace CompanyManager.Tests.Services.Companies
{
    public class CompanyLogicTests
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())));

        public CompanyLogicTests()
        {
        }

        private CompanyDbContext GetDbContext()
            => new CompanyDbContext(
                new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private readonly IEnumerable<Company> testData = new List<Company>
        {
            //{ new Employee {FirstName = "Lidia", LastName="Georgieva",ExperienceLevel = new ExperienceLevel(), Salary = 1000, StartingDate = DateTime.Parse("11/11/2018", CultureInfo.CurrentCulture), VacationDays=25 }
            new Company{ Id = 4, Name = "Lidivo1", Description = "Grocery store1", Headquarters = "Sofia1", Employees = new List<Employee>()},
            new Company{ Id = 2, Name = "Lidivo2", Description = "Grocery store2", Headquarters = "Sofia2", Employees = new List<Employee>()},
            new Company{ Id = 5, Name = "Lidivo3", Description = "Grocery store3", Headquarters = "Sofia3",Employees  = new List<Employee>()},
            new Company{ Id = 1, Name = "Lidivo4", Description = "Grocery store4", Headquarters = "Sofia4", Employees = new List<Employee>()},
            new Company{ Id = 3, Name = "Lidivo5", Description = "Grocery store5", Headquarters = "Sofia5", Employees = new List<Employee>()}
        };

        private void PopulateData(CompanyDbContext db)
        {
            db.AddRange(this.testData);
            db.SaveChanges();
        }

        private bool CompareCompaniesWithMemeListingServiceModelExact(CompanyListingViewModel thisCompany, Company otherCompany)
           => thisCompany.Id == otherCompany.Id
           && thisCompany.Name == otherCompany.Name
           && thisCompany.Description == otherCompany.Description
           && thisCompany.Headquarters == otherCompany.Headquarters;

        [Fact]
        public void GetCompany_ShouldReturnCorrectRecord_WhenExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new CompanyLogic(context, mapper);
            var company = service.GetCompany(1);

            // Assert
            Assert.NotNull(company);
            Assert.Equal("Lidivo4", company.Name);
        }

        [Fact]
        public void GetCompany_ShouldReturnCorrectRecord_WhenNotExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new CompanyLogic(context, mapper);
            var company = service.GetCompany(10);

            // Assert
            Assert.Null(company);
        }

        [Fact]
        public void EditCompany_ShouldEditRecord()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new CompanyLogic(context, mapper);
            var newCompany = new CompanyDTO
            {
                Id = 5,
                Name = "Lidivo3 Edited",
                Description = "Grocery store3",
                Headquarters = "Sofia3",
                Employees = new List<EmployeeDTO>()
            };

            service.EditCompany(newCompany);
            var company = context.Companies.Find(5);

            // Assert
            Assert.NotNull(company);
            Assert.Equal("Lidivo3 Edited", company.Name);
        }

        [Fact]
        public void DeleteCompany_ShouldDeleteRecord_WhenExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new CompanyLogic(context, mapper);
            var oldCompany = context.Companies.Find(5);
            service.DeleteCompany(oldCompany.Id);

            var company = context.Companies.Find(5);
            // Assert
            Assert.NotNull(oldCompany);
            Assert.False(oldCompany.IsActive);
        }

        [Fact]
        public void DeleteCompany_ShouldDeleteRecord_WhenNotExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);
            Exception exception = null;

            // Act
            var service = new CompanyLogic(context, mapper);
            var oldCompany = context.Companies.Find(50);
            
            try
            {
                service.DeleteCompany(oldCompany.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
        }
    }
}
