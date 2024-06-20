using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Template.Net.Microservice.Test.Definitions.Mapping;

public class AutoMapperDefinition : ApplicationDefinition
{ 
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
        => context.ServiceCollection.AddAutoMapper(typeof(Program));
    
    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var mapper = context.ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
        mapper.AssertConfigurationIsValid();
    }
}