using MassTransit;
using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;

namespace MicroserviceTemplate.Api.Definitions.MassTransit;

public class MassTransitDefinition : Definition
{
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq();
        });
    }
}