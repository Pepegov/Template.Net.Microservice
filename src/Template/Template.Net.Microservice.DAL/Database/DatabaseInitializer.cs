using Microsoft.EntityFrameworkCore;

namespace Template.Net.Microservice.DAL.Database
{
    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public DatabaseInitializer(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }
        
        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            //TODO if you are not using migrations, then uncomment this line
            //await _context!.Database.EnsureCreatedAsync(cancellationToken);
            var pending = await _context.Database.GetPendingMigrationsAsync(cancellationToken: cancellationToken); 
            if (pending.Any())
            {
                await _context!.Database.MigrateAsync(cancellationToken: cancellationToken);
            }
        }
        
        public void Seed()
        {
            _context!.Database.EnsureCreated();
            var pending = _context.Database.GetPendingMigrations(); 
            if (pending.Any())
            {
                _context!.Database.Migrate();
            }
        }
    }
}
