using AutoMapper;
using CompanyManager.BLL;
using CompanyManager.BLL.Interfaces;
using CompanyManager.CompanyMicroservice.Infrastructure.Mapping;
using CompanyManager.DAL;
using CompanyManager.Models.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CompanyManager.CompanyMicroservice
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CompanyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CompanyManagerDb")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IServiceBusResponder, ServiceBusResponder>();
            services.AddTransient<ICompanyLogic, CompanyLogic>();
            services.AddTransient<IEmployeeLogic, EmployeeLogic>();
            
            // Cause a problem when AddMigration is runned in PackageManagerConsole!!!
            IBus bus = RabbitHutch.CreateBus("host=localhost");
            services.AddSingleton(bus);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseDatabaseMigration();
            
            var serviceBusResponder = app.ApplicationServices.GetService<IServiceBusResponder>();
            serviceBusResponder.SetUp();
            
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
