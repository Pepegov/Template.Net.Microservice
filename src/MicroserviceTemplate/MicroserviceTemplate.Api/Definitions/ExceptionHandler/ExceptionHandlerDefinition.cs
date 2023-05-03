using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;
using Pepegov.MicroserviceFramerwork.AspNetCore.Filters;
using Pepegov.MicroserviceFramerwork.AspNetCore.Middleware;

namespace MicroserviceTemplate.Api.Definitions.ExceptionHandler;

public class ExceptionHandlerDefinition : Definition
{
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMvc(options => options.Filters.Add<GlobalExceptionFilter>());
    }

    public override void ConfigureApplicationAsync(WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}