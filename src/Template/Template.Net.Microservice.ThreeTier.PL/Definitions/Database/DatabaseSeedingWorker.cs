using Template.Net.Microservice.ThreeTier.DAL.Database;

namespace Template.Net.Microservice.ThreeTier.PL.Definitions.Database;

/// <summary>
/// Worker that apply database migrations and seeding data
/// </summary>
/// <param name="serviceProvider"></param>
public class DatabaseSeedingWorker(IServiceProvider serviceProvider) : IHostedService
{
    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        await new DatabaseInitializer(serviceProvider, dbContext).SeedAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}