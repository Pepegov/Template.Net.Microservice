using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Template.Net.Microservice.ThreeTier.DAL.Database;
using Template.Net.Microservice.ThreeTier.DAL.Domain;

namespace Template.Net.Microservice.ThreeTier.UnitTest.Definitions.Database
{
    public class DatabaseDefinition : ApplicationDefinition
    {
        public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
        {
            string migrationsAssembly = typeof(DIProvider).GetTypeInfo().Assembly.GetName().Name!;
            string connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                ?? $"Server=localhost;Port=5432;User Id=postgres;Password=password;Database={AppData.ServiceName}";

            context.ServiceCollection.AddDbContext<TestDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
