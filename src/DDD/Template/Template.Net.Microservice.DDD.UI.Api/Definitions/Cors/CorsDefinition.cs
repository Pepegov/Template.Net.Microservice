using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Template.Net.Microservice.DDD.Application;

namespace Template.Net.Microservice.DDD.UI.Api.Definitions.Cors;

/// <summary>
/// Cors configurations
/// </summary>
public class CorsDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        var origins = context.Configuration.GetSection("Cors")?.GetSection("Origins")?.Value?.Split(',');
        context.ServiceCollection.AddCors(options =>
        {
            options.AddPolicy(AppData.PolicyName, builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                if (origins is not { Length: > 0 })
                {
                    return;
                }

                if (origins.Contains("*"))
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.SetIsOriginAllowed(host => true);
                    builder.AllowCredentials();
                }
                else
                {
                    foreach (var origin in origins)
                    {
                        builder.WithOrigins(origin);
                    }
                }
            });
        });

        return base.ConfigureServicesAsync(context);
    }
}