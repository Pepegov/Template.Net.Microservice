﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Template.Net.Microservice.DDD.Application;
using Template.Net.Microservice.DDD.Application.Database;

namespace Template.Net.Microservice.DDD.UI.Api.Definitions.Database;

/// <summary>
/// EF database content registration
/// </summary>
public class DatabaseDefinition : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        string migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name!;
        string connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                                  ?? $"Server=localhost;Port=5432;User Id=postgres;Password=password;Database={AppData.ServiceName}";

        context.ServiceCollection.AddDbContext<ApplicationDbContext>(options =>
            //TODO: change your db provider 
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)));

        return base.ConfigureServicesAsync(context);
    }
}
