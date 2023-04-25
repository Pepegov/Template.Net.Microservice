using MassTransit;
using MicroserviceTemplate.Base.Definition;

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