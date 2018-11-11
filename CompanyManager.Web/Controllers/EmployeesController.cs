using AutoMapper;
using CompanyManager.Models.DTOs.Employees;
using CompanyManager.Models.Messages.Employees;
using CompanyManager.Web.Models.Employees;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IBus bus;
        private readonly IMapper mapper;

        public EmployeesController(IBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }
        
        public IActionResult Add([FromRoute]int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromRoute]int id, AddEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if model state is not valid, redirect user to AddCompany page and list validation errors
                return BadRequest(ModelState);
            }

            var addEmployeeDto = mapper.Map<AddEmployeeDTO>(model);
            var message = new AddEmployeeMessage
            {
                Employee = addEmployeeDto,
                CompanyId = id
            };

            bus.Request<AddEmployeeMessage, AddEmployeeResponseMessage>(message);

            return RedirectToAction("Details", "Companies", new { id });
        }


        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var message = new GetEmployeeMessage { Id = id };
            var responseMessage = bus.Request<GetEmployeeMessage, GetEmployeeResponseMessage>(message);
            var editEmployeeViewModel = mapper.Map<EmployeeViewModel>(responseMessage.Employee);

            return View(editEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var editEmployee = mapper.Map<EmployeeDTO>(model);

            var message = new EditEmployeeMessage { Employee = editEmployee };

            var responseMessage = bus.Request<EditEmployeeMessage, EditEmployeeResponseMessage>(message);

            return RedirectToAction("All", "Companies");
        }

        public IActionResult Delete([FromRoute]int id)
        {
            var message = new DeleteEmployeeMessage()
            {
                EmployeeId = id
            };
            bus.Request<DeleteEmployeeMessage, DeleteEmployeeResponseMessage>(message);

            return RedirectToAction("All", "Companies");
        }
    }
}
