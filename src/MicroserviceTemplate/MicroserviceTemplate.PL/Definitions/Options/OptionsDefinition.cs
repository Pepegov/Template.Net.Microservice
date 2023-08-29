using MicroserviceTemplate.DAL.Domain;
using MicroserviceTemplate.PL.Definitions.Options.Models;
using Pepegov.MicroserviceFramerwork.AspNetCore.Definition;

namespace MicroserviceTemplate.PL.Definitions.Options;

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
