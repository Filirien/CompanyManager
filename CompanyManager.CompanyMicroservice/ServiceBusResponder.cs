using CompanyManager.BLL.Interfaces;
using CompanyManager.Models.Messages.Companies;
using CompanyManager.Models.Messages.Employees;
using EasyNetQ;

namespace CompanyManager.CompanyMicroservice
{
    public class ServiceBusResponder : IServiceBusResponder
    {
        private readonly IBus bus;

        private readonly ICompanyLogic companyLogic;

        private readonly IEmployeeLogic employeeLogic;

        public ServiceBusResponder(IBus bus, ICompanyLogic companyLogic, IEmployeeLogic employeeLogic)
        {
            this.bus = bus;
            this.companyLogic = companyLogic;
            this.employeeLogic = employeeLogic;
        }

        public void SetUp()
        {
            bus.Respond<AddCompanyMessage, AddCompanyResponseMessage>(OnAddCompanyMessage);

            bus.Respond<GetCompanyMessage, GetCompanyResponseMessage>(OnGetCompanyMessage);

            bus.Respond<EditCompanyMessage, EditCompanyResponseMessage>(OnEditCompanyMessage);

            bus.Respond<AllCompaniesMessage, AllCompaniesResponseMessage>(OnAllCompaniesMessage);

            bus.Respond<DeleteCompanyMessage, DeleteCompanyResponseMessage>(OnDeleteCompanyMessage);

            bus.Respond<GetEmployeeMessage, GetEmployeeResponseMessage>(OnGetEmployeeMessage);
            
            bus.Respond<AddEmployeeMessage, AddEmployeeResponseMessage>(OnAddEmployeeMessage);

            bus.Respond<EditEmployeeMessage, EditEmployeeResponseMessage>(OnEditEmployeeMessage);

            bus.Respond<DeleteEmployeeMessage, DeleteEmployeeResponseMessage>(OnDeleteEmployeeMessage);
        }

        private AddCompanyResponseMessage OnAddCompanyMessage(AddCompanyMessage message)
        {
            var company = companyLogic.AddCompany(message.Company);

            var responseMessage = new AddCompanyResponseMessage
            {
                Company = company
            };

            return responseMessage;
        }

        private GetCompanyResponseMessage OnGetCompanyMessage(GetCompanyMessage message)
        {
            var company = companyLogic.GetCompany(message.CompanyId);

            var responseMessage = new GetCompanyResponseMessage
            {
                Company = company
            };

            return responseMessage;
        }

        private EditCompanyResponseMessage OnEditCompanyMessage(EditCompanyMessage message)
        {
            var editedCompany = companyLogic.EditCompany(message.Company);

            var responseMessage = new EditCompanyResponseMessage
            {
                Company = editedCompany
            };

            return responseMessage;
        }

        private AllCompaniesResponseMessage OnAllCompaniesMessage(AllCompaniesMessage message)
        {
            var companies = companyLogic.AllCompanies();

            var responseMessage = new AllCompaniesResponseMessage
            {
                Companies = companies
            };

            return responseMessage;
        } 

        private DeleteCompanyResponseMessage OnDeleteCompanyMessage(DeleteCompanyMessage message)
        {
            companyLogic.DeleteCompany(message.CompanyId);
            var responseMessage = new DeleteCompanyResponseMessage();

            return responseMessage;
        }

        private GetEmployeeResponseMessage OnGetEmployeeMessage(GetEmployeeMessage message)
        {
            var employee = employeeLogic.GetEmployee(message.Id);

            var responseMessage = new GetEmployeeResponseMessage
            {
                Employee = employee
            };

            return responseMessage;

        }

        private AddEmployeeResponseMessage OnAddEmployeeMessage(AddEmployeeMessage message)
        {
            employeeLogic.AddEmployee(message.CompanyId, message.Employee);
            var responseMessage = new AddEmployeeResponseMessage();

            return responseMessage;
        }
               
        private EditEmployeeResponseMessage OnEditEmployeeMessage(EditEmployeeMessage message)
        {
            var editedEmployee = employeeLogic.EditEmployee(message.Employee);

            var responseMessage = new EditEmployeeResponseMessage
            {
                Employee = editedEmployee
            };

            return responseMessage;
        }

        private DeleteEmployeeResponseMessage OnDeleteEmployeeMessage(DeleteEmployeeMessage message)
        {
            employeeLogic.DeleteEmployee(message.EmployeeId);
            var responseMessage = new DeleteEmployeeResponseMessage();

            return responseMessage;
        }
    }
}

