using MicroserviceTemplate.Api.Definitions.Options.Models;
using MicroserviceTemplate.DAL.Domain;
using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;

namespace MicroserviceTemplate.Api.Definitions.Options;

public class OptionsDefinition : Definition
{
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        var identityConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppData.IdentitySettingPath)
            .Build();
        
        services.Configure<IdentityAddressOption>(identityConfiguration.GetSection("IdentityServerUrl"));
        services.Configure<IdentityClientOption>(identityConfiguration.GetSection("CurrentIdentityClient"));
    }
}
