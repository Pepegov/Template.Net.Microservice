using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;

namespace MicroserviceTemplate.PL.Definitions.Mapping;

public class AutoMapperDefinition : Definition
{ 
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        => services.AddAutoMapper(typeof(Program));
    
    public override void ConfigureApplicationAsync(WebApplication app)
    {
        var mapper = app.Services.GetRequiredService<AutoMapper.IConfigurationProvider>();
        if (app.Environment.IsDevelopment())
        {
            mapper.AssertConfigurationIsValid();
        }
        else
        {
            mapper.CompileMappings();
        }
    }
}