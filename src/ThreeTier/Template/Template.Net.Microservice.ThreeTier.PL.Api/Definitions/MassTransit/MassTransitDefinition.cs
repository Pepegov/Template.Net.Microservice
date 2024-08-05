using System.Reflection;
using MassTransit;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Pepegov.MicroserviceFramework.Exceptions;

namespace Template.Net.Microservice.ThreeTier.PL.Api.Definitions.MassTransit;

/// <summary>
/// Registration masstransit for mq support
/// </summary>
public class MassTransitDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override bool Enabled => false; //TODO: set "true" value or remove this line for enable this definition if you need

    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext definitionContext)
    {
        definitionContext.ServiceCollection.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();
            
            var assembly = Assembly.GetEntryAssembly();
            var setting = definitionContext.Configuration.GetSection("RabbitMQ").Get<MassTransitOption>();
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

        return base.ConfigureServicesAsync(definitionContext);
    }
}