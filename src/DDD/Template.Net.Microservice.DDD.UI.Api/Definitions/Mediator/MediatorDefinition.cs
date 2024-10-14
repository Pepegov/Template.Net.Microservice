using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Template.Net.Microservice.DDD.UI.Api.Definitions.Mediator;

/// <summary>
/// Register Mediator as application definition
/// </summary>
public class MediatorDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        context.ServiceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        return base.ConfigureServicesAsync(context);
    }
}