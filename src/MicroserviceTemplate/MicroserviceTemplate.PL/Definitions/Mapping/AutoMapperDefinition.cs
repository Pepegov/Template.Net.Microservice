using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace MicroserviceTemplate.PL.Definitions.Mapping;

public class AutoMapperDefinition : ApplicationDefinition
{ 
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
        => context.ServiceCollection.AddAutoMapper(typeof(Program));
    
    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        
        var mapper = webContext.ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
        if (webContext.WebApplication.Environment.IsDevelopment())
        {
            mapper.AssertConfigurationIsValid();
        }
        else
        {
            mapper.CompileMappings();
        }
    }
}