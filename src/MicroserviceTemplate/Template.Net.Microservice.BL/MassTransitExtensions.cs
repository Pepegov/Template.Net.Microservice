using System.Reflection;
using MassTransit;
using Pepegov.MicroserviceFramework.Data;

namespace MicroserviceTemplate.BL;

public static class MassTransitExtensions
{
    public static void AddRequestClientContracts(this IBusRegistrationConfigurator configurator, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var exportedType in assembly.ExportedTypes)
            {
                var interfaces = exportedType.GetInterfaces();
                if(interfaces.Contains(typeof(IContract)))
                {
                    configurator.AddRequestClient(exportedType);
                }
            }
        }
    }
}