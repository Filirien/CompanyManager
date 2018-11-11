using AutoMapper;
using CompanyManager.BLL;
using CompanyManager.CompanyMicroservice.Infrastructure.Mapping;
using CompanyManager.DAL;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Employees;
using CompanyManager.Models.Enums;
using CompanyManager.Web.Models.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CompanyManager.Tests.Services.Employees
{
    public class EmployeeLogicTests
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())));

        public EmployeeLogicTests()
        {
        }

        private CompanyDbContext GetDbContext()
            => new CompanyDbContext(
                new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private readonly IEnumerable<Employee> testData = new List<Employee>
        {
            //{ new Employee {FirstName = "Lidia", LastName="Georgieva",ExperienceLevel = new ExperienceLevel(), Salary = 1000, StartingDate = DateTime.Parse("11/11/2018", CultureInfo.CurrentCulture), VacationDays=25 }
            new Employee{ Id = 4, FirstName = "Pesho1", LastName = "Grishov1", ExperienceLevel = (ExperienceLevel)1, Salary = 1000, StartingDate = DateTime.Now },
            new Employee{ Id = 2, FirstName = "Pesho2", LastName = "Grishov2", ExperienceLevel = (ExperienceLevel)2, Salary = 1000, StartingDate = DateTime.Now },
            new Employee{ Id = 5, FirstName = "Pesho3", LastName = "Grishov3", ExperienceLevel = (ExperienceLevel)3, Salary = 1000, StartingDate = DateTime.Now },
            new Employee{ Id = 1, FirstName = "Pesho4", LastName = "Grishov4", ExperienceLevel = (ExperienceLevel)4, Salary = 1000, StartingDate = DateTime.Now },
            new Employee{ Id = 3, FirstName = "Pesho5", LastName = "Grishov5", ExperienceLevel = (ExperienceLevel)5, Salary = 1000,  StartingDate = DateTime.Now }
        };

        private void PopulateData(CompanyDbContext db)
        {
            db.AddRange(this.testData);
            db.SaveChanges();
        }

        private bool CompareCompaniesWithMemeListingServiceModelExact(EmployeeViewModel thisEmployee, Employee otherEmployee)
           => thisEmployee.FirstName == otherEmployee.FirstName
           && thisEmployee.LastName == otherEmployee.LastName
           && thisEmployee.Salary == otherEmployee.Salary
           && thisEmployee.StartingDate == otherEmployee.StartingDate
           && thisEmployee.ExperienceLevel == otherEmployee.ExperienceLevel;

        [Fact]
        public void GetEmployee_ShouldReturnCorrectRecord_WhenExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new EmployeeLogic(context, mapper);
            var employee = service.GetEmployee(1);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("Pesho4", employee.FirstName);
        }

        [Fact]
        public void GetEmployee_ShouldReturnCorrectRecord_WhenNotExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new EmployeeLogic(context, mapper);
            var employee = service.GetEmployee(10);

            // Assert
            Assert.Null(employee);
        }

        [Fact]
        public void EditEmployee_ShouldEditRecord()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);

            // Act
            var service = new EmployeeLogic(context, mapper);
            var newEmployee = new EmployeeDTO
            {
                Id = 5,
                FirstName = "Pesho3 Edited",
                LastName = "Grishov3",
                Salary = 1900,
                ExperienceLevel = (ExperienceLevel)4,
                StartingDate = DateTime.Now
            };

            service.EditEmployee(newEmployee);

            var employee = context.Employees.Find(5);
            // Assert
            Assert.NotNull(employee);
            Assert.Equal("Pesho3 Edited", employee.FirstName);
        }

        [Fact]
        public void DeleteEmployee_ShouldDeleteRecord_WhenExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);
            Exception exception = null;
            // Act
            var service = new EmployeeLogic(context, mapper);
            var oldEmployee = context.Companies.Find(4);

            try
            {
                service.DeleteEmployee(oldEmployee.Id);
                var employee = context.Employees.Find(4);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void DeleteEmployee_ShouldDeleteRecord_WhenNotExistingIdIsPassed()
        {
            // Arrange
            var context = this.GetDbContext();
            this.PopulateData(context);
            Exception exception = null;
            // Act
            var service = new EmployeeLogic(context, mapper);
            var oldEmployee = context.Companies.Find(40);

            try
            {
                service.DeleteEmployee(oldEmployee.Id);
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
