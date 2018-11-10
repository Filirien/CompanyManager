using AutoMapper;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.Messages.Companies;
using CompanyManager.Web.Models.Companies;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Web.Controllers
{
    public class CompaniesController : Controller
    {

        private readonly IBus bus;
        private readonly IMapper mapper;

        public CompaniesController(IBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if model state is not valid, redirect user to AddCompany page and list validation errors
            }

            var addCompanyDto = mapper.Map<AddCompanyDTO>(model);
            var message = new AddCompanyMessage { Company = addCompanyDto };

            var result = bus.Request<AddCompanyMessage, AddCompanyResponseMessage>(message);

            return RedirectToAction("Details", "Companies", new { id = result.Company.Id });
        }
    }
}
