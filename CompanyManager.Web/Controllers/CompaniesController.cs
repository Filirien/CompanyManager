using AutoMapper;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.Messages.Companies;
using CompanyManager.Web.Models.Companies;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
                return BadRequest(ModelState);
            }

            var addCompanyDto = mapper.Map<AddCompanyDTO>(model);
            var message = new AddCompanyMessage { Company = addCompanyDto };

            var responseMessage = bus.Request<AddCompanyMessage, AddCompanyResponseMessage>(message);

            return RedirectToAction("Details", "Companies", new { id = responseMessage.Company.Id });
        }

        [HttpGet]
        public IActionResult Details([FromRoute]int id)
        {
            var message = new GetCompanyMessage { CompanyId = id };

            var responseMessage = bus.Request<GetCompanyMessage, GetCompanyResponseMessage>(message);
            var companyDetailsViewModel = mapper.Map<CompanyDetailsViewModel>(responseMessage.Company);

            return View(companyDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var message = new GetCompanyMessage { CompanyId = id };
            var responseMessage = bus.Request<GetCompanyMessage, GetCompanyResponseMessage>(message);
            var editCompanyViewModel = mapper.Map<EditCompanyViewModel>(responseMessage.Company);

            return View(editCompanyViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if model state is not valid, redirect user to AddCompany page and list validation errors
                return BadRequest(ModelState);
            }

            var editCompany = mapper.Map<CompanyDTO>(model);

            var message = new EditCompanyMessage { Company = editCompany };

            var responseMessage = bus.Request<EditCompanyMessage, EditCompanyResponseMessage>(message);
           
            return RedirectToAction("Details", "Companies", new { id = responseMessage.Company.Id });
        }


        public IActionResult All()
        {
            var message = new AllCompaniesMessage();

            var responseMessage = bus.Request<AllCompaniesMessage, AllCompaniesResponseMessage>(message);
            var companyListingViewModel = mapper.Map<List<CompanyListingViewModel>>(responseMessage.Companies);
            
            return View(companyListingViewModel);
        }
        
        public IActionResult Delete([FromRoute]int id)
        {
            var message = new DeleteCompanyMessage()
            {
                CompanyId = id
            };
            bus.Request<DeleteCompanyMessage, DeleteCompanyResponseMessage>(message);
            return RedirectToAction("All", "Companies");
        }        
    }
}
