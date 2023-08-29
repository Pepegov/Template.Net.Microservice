using System.Reflection;
using MassTransit;
using MicroserviceTemplate.PL.Definitions.Options.Models;
using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;
using Pepegov.MicroserviceFramerwork.Exceptions;

namespace MicroserviceTemplate.PL.Definitions.MassTransit;

public class MassTransitDefinition : Definition
{
    public override bool Enabled => false;

    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();
            
            var assembly = Assembly.GetEntryAssembly();
            var setting = builder.Configuration.GetSection("RabbitMQ").Get<MassTransitOption>();
            if (setting is null)
            {
                throw new MicroserviceArgumentNullException("MassTransit setting is null");
            }
            
            x.AddConsumers(assembly);
            x.AddSagaStateMachines(assembly);
            x.AddSagas(assembly);
            x.AddActivities(assembly);
            
            x.UsingRabbitMq((context, cfg) =>
            {

                cfg.Host($"rabbitmq://{setting.Url}/{setting.Host}", h =>
                {
                    h.Username(setting.User);
                    h.Password(setting.Password); 
                });

                cfg.ConfigureEndpoints(context);
            });

        });
    }
}