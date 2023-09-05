using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace MicroserviceTemplate.PL.Definitions.OpenIddict;

public class OpenIddictDefinition : ApplicationDefinition
{
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context) => 
        context.ServiceCollection
            .AddOpenIddict()
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
}