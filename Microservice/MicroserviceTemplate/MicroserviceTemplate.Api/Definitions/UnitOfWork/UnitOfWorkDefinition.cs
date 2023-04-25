using MicroserviceTemplate.Base.Definition;
using MicroserviceTemplate.Base.UnitOfWork;
using MicroserviceTemplate.DAL.Database;

namespace MicroserviceTemplate.Api.Definitions.UnitOfWork
{
    public class UnitOfWorkDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddUnitOfWork<ApplicationDbContext>();
        }
    }
}
