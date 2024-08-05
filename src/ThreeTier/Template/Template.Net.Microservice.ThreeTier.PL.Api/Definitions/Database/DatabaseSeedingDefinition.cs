using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Template.Net.Microservice.ThreeTier.PL.Api.Definitions.Database;

/// <summary>
/// Registration seeding worker
/// </summary>
public class DatabaseSeedingDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.ServiceCollection.AddHostedService<DatabaseSeedingWorker>();
        return base.ConfigureServicesAsync(context);
    }
}
