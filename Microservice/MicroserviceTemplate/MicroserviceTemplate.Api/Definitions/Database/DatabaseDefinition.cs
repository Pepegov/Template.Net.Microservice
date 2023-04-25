using System.Reflection;
using MicroserviceTemplate.Base.Definition;
using MicroserviceTemplate.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTemplate.Api.Definitions.Database
{
    public class DatabaseDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            string migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name!;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=Microservice";

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
