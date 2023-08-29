using MicroserviceTemplate.DAL.Database;
using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;
using Pepegov.UnitOfWork;
using Pepegov.UnitOfWork.EntityFramework.Configuration;

namespace MicroserviceTemplate.PL.Definitions.UnitOfWork
{
    public class UnitOfWorkDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddUnitOfWork(x =>
            {
                x.UsingEntityFramework((context, configurator) =>
                {
                    configurator.DatabaseContext<ApplicationDbContext>();
                });
            });        
        }
    }
}
