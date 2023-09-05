using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace MicroserviceTemplate.PL.Definitions.Base;

/// <summary>
/// AspNetCore common configuration
/// </summary>
public class CommonDefinition : ApplicationDefinition
{
    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        webContext.WebApplication.UseHttpsRedirection();
    }

    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.ServiceCollection.AddLocalization();
        context.ServiceCollection.AddHttpContextAccessor();
        context.ServiceCollection.AddResponseCaching();
    }
}