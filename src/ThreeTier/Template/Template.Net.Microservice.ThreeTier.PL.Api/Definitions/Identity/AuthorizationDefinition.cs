using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Server.AspNetCore;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Template.Net.Microservice.ThreeTier.DAL.Domain;
using Template.Net.Microservice.ThreeTier.PL.Api.Definitions.Identity.Options;
using Template.Net.Microservice.ThreeTier.PL.Api.Definitions.OpenIddict;

namespace Template.Net.Microservice.ThreeTier.PL.Api.Definitions.Identity;

/// <summary>
/// Authorization Policy registration
/// </summary>
public class AuthorizationDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext definitionContext)
    {
        var url = definitionContext.Configuration.GetSection("IdentityServerUrl").GetValue<string>("Authority");
        var currentClient = definitionContext.Configuration.GetSection("CurrentIdentityClient").Get<IdentityClientOption>()!;
        
        definitionContext.ServiceCollection
            .AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, "Bearer", options =>
            {
                options.SaveToken = true;
                options.Audience = currentClient.Id;
                options.Authority = url;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, // Audience should be defined on the authorization server or disabled as shown
                    ClockSkew = new TimeSpan(0, 0, 30)
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Ensure we always have an error and error description.
                        if (string.IsNullOrEmpty(context.Error))
                        {
                            context.Error = "invalid_token";
                        }

                        if (string.IsNullOrEmpty(context.ErrorDescription))
                        {
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                        }

                        // Add some extra context for expired tokens.
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Append("x-token-expired", authenticationException?.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException?.Expires:o}";
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            error = context.Error,
                            error_description = context.ErrorDescription
                        }));
                    }
                };
            });

        definitionContext.ServiceCollection.AddAuthorization(options =>
        {
            options.AddPolicy(AuthData.AuthenticationSchemes, policy =>
            {
                policy.RequireAuthenticatedUser();
                //policy.RequireClaim("scope", "api");
            });
        });

        definitionContext.ServiceCollection.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        definitionContext.ServiceCollection.AddSingleton<IAuthorizationHandler, AppPermissionHandler>();

        
        definitionContext.ServiceCollection.Configure<IdentityAddressOption>(definitionContext.Configuration.GetSection("IdentityServerUrl"));
        definitionContext.ServiceCollection.Configure<IdentityClientOption>(definitionContext.Configuration.GetSection("CurrentIdentityClient"));
        
        return base.ConfigureServicesAsync(definitionContext);
    }

    /// <inheritdoc />
    public override Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        
        webContext.WebApplication.UseRouting();
        webContext.WebApplication.UseCors(AppData.PolicyName);
        webContext.WebApplication.UseAuthentication();
        webContext.WebApplication.UseAuthorization();

        // registering UserIdentity helper as singleton
        UserIdentity.Instance.Configure(webContext.WebApplication.Services.GetService<IHttpContextAccessor>()!);

        return base.ConfigureApplicationAsync(context);
    }
}