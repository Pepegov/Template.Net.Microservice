using MicroserviceTemplate.DAL.Domain;
using MicroserviceTemplate.PL.Definitions.Options.Models;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace MicroserviceTemplate.PL.Definitions.Options;

public class OptionsDefinition : ApplicationDefinition
{
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.ServiceCollection.Configure<IdentityAddressOption>(context.Configuration.GetSection("IdentityServerUrl"));
        context.ServiceCollection.Configure<IdentityClientOption>(context.Configuration.GetSection("CurrentIdentityClient"));
    }
}
