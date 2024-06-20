using Microsoft.EntityFrameworkCore;

namespace MicroserviceTemplate.DAL.Database
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
        
        public async Task SeedAsync()
        {
            await _context!.Database.EnsureCreatedAsync();
            var pending = await _context.Database.GetPendingMigrationsAsync(); 
            if (pending.Any())
            {
                await _context!.Database.MigrateAsync();
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
