using MicroserviceTemplate.Api.Definitions.Options.Models;
using MicroserviceTemplate.Base.Definition;

namespace MicroserviceTemplate.Api.Definitions.Options;

public class OptionsDefinition : Definition
{
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddJsonFile("identitysetting.json");
        IConfiguration identityConfiguration = configurationBuilder.Build();

        services.Configure<IdentityAddressOption>(identityConfiguration.GetSection("IdentityServerUrl"));
        services.Configure<IdentityClientOption>(identityConfiguration.GetSection("CurrentIdentityClient"));
    }
}
