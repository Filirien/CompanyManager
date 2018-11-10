using AutoMapper;
using CompanyManager.BLL.Interfaces;
using CompanyManager.DAL.Entities;
using CompanyManager.Models.DTOs.Companies;
using CompanyManager.Models.Messages.Companies;
using EasyNetQ;

namespace CompanyManager.CompanyMicroservice
{
    public class ServiceBusResponder : IServiceBusResponder
    {
        private readonly IBus bus;

        private readonly ICompanyLogic companyLogic;

        private readonly IMapper mapper;

        public ServiceBusResponder(IBus bus, ICompanyLogic companyLogic, IMapper mapper)
        {
            this.bus = bus;
            this.companyLogic = companyLogic;
            this.mapper = mapper;
        }

        public void SetUp()
        {
            bus.Respond<AddCompanyMessage, AddCompanyResponseMessage>(AddCompanyMessageHandler);
        }

        private AddCompanyResponseMessage AddCompanyMessageHandler(AddCompanyMessage message)
        {
            var newCompany = mapper.Map<Company>(message.Company);

            var company = companyLogic.AddCompany(newCompany);

            var responseMessage = new AddCompanyResponseMessage
            {
                Company = mapper.Map<CompanyDTO>(company)
            };

            return responseMessage;
        }
    }
}
