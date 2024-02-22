using System.Reflection;
using MicroserviceTemplate.DAL.Database;
using MicroserviceTemplate.DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace MicroserviceTemplate.PL.Definitions.Database
{
    public class DatabaseDefinition : ApplicationDefinition
    {
        public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
        {
            string migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name!;
            string connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                ?? $"Server=localhost;Port=5432;User Id=postgres;Password=password;Database={AppData.ServiceName}";

            context.ServiceCollection.AddDbContext<ApplicationDbContext>(options =>
            //TODO: change your db provider 
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
