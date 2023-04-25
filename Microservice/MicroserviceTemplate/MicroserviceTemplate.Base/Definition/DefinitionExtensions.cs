using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceTemplate.Base.Definition;

public static class DefinitionExtensions
{
    public static void AddDefinitions(this IServiceCollection services, WebApplicationBuilder builder,
        params Type[] entryPointsAssembly)
    {
        var definitions = new List<IDefinition>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(t =>
                !t.IsAbstract && typeof(IDefinition).IsAssignableFrom(t));
            var instances = types.Select(Activator.CreateInstance).Cast<IDefinition>();
            var list = instances.Where(x => x.Enabled == true);
            definitions.AddRange(list);
        }

        definitions.ForEach(definition => definition.ConfigureServicesAsync(services, builder));
        services.AddSingleton(definitions as IReadOnlyCollection<IDefinition>);
    }

    public static void UseDefinitions(this WebApplication application)
    {
        var definitions = application.Services.GetRequiredService<IReadOnlyCollection<IDefinition>>();
        foreach (var endpoint in definitions)
        {
            endpoint.ConfigureApplicationAsync(application);
        }
    }
}