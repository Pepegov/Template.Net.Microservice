using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Pepegov.MicroserviceFramework.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerUI;
using Template.Net.Microservice.DDD.Application;
using Template.Net.Microservice.ThreeTier.PL.Definitions.Identity.Options;

namespace Template.Net.Microservice.DDD.UI.Api.Definitions.Swagger;

/// <summary>
/// Swagger definition for application
/// </summary>
public class SwaggerDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        
        using var scope = webContext.WebApplication.Services.CreateAsyncScope();
        var client = scope.ServiceProvider.GetService<IOptions<IdentityClientOption>>()!.Value;
        
        webContext.WebApplication.UseSwagger();
        webContext.WebApplication.UseSwaggerUI(settings =>
        {
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DefaultModelsExpandDepth(0);
            settings.DocExpansion(DocExpansion.None);
            settings.OAuthScopeSeparator(" ");
            settings.OAuthClientId(client.Id);
            settings.OAuthClientSecret(client.Secret);
            settings.DisplayRequestDuration();
        });

        return base.ConfigureApplicationAsync(context);
    }

    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.ServiceCollection.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        context.ServiceCollection.AddEndpointsApiExplorer();
        context.ServiceCollection.AddSwaggerGen(options =>
        {
            var now = DateTime.Now.ToString("f");
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = AppData.ServiceName,
                Version = AppData.ServiceVersion,
                Description = AppData.ServiceDescription + $" | Upload time: {now}"
            });

            options.ResolveConflictingActions(x => x.First());

            var filePath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
            options.IncludeXmlComments(filePath);
            
            options.TagActionsBy(api =>
            {
                string tag;
                if (api.ActionDescriptor is { } descriptor)
                {
                    var attribute = descriptor.EndpointMetadata.OfType<FeatureGroupNameAttribute>().FirstOrDefault();
                    tag = attribute?.GroupName ?? descriptor.RouteValues["controller"] ?? "Untitled";
                }
                else
                {
                    tag = api.RelativePath!;
                }

                var tags = new List<string>();
                if (!string.IsNullOrEmpty(tag))
                {
                    tags.Add(tag);
                }
                return tags;
            });
            
            var url = context.Configuration.GetSection("IdentityServerUrl").GetValue<string>("Authority");
            var currentClient = context.Configuration.GetSection("CurrentIdentityClient").Get<IdentityClientOption>()!;
            var scopes = currentClient.Scopes!.ToDictionary(x => x, x => x);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{url}/connect/authorize", UriKind.Absolute),
                        TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                        Scopes = scopes,
                    },
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        Scopes = scopes,
                        TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                    },
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                        Scopes = scopes,
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        return base.ConfigureServicesAsync(context);
    }
}