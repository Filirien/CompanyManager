using CompanyManager.Web.Models.Employees;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private IBus bus;

        public EmployeesController(IBus bus)
        {
            this.bus = bus;
        }
        
    }
}
