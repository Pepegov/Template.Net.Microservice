using Microsoft.EntityFrameworkCore;

namespace Template.Net.Microservice.ThreeTier.DAL.Database;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}