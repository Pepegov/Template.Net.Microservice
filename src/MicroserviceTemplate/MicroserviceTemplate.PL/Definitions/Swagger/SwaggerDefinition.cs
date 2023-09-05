using MicroserviceTemplate.DAL.Domain;
using MicroserviceTemplate.PL.Definitions.Options.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Pepegov.MicroserviceFramework.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MicroserviceTemplate.PL.Definitions.Swagger;

/// <summary>
/// Swagger definition for application
/// </summary>
public class SwaggerDefinition : ApplicationDefinition
{
    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        
        if (!webContext.WebApplication.Environment.IsDevelopment())
        {
            return;
        }

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
    }

    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
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
            
            var identityConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppData.IdentitySettingPath)
                .Build();
            
            var url = identityConfiguration.GetSection("IdentityServerUrl").GetValue<string>("Authority");
            var currentClient = identityConfiguration.GetSection("CurrentIdentityClient").Get<IdentityClientOption>()!;
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
    }
}