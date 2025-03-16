using Microsoft.EntityFrameworkCore;
using Template.Net.Microservice.DDD.Domain.Aggregate;
using Template.Net.Microservice.DDD.Domain.Entity;

namespace Template.Net.Microservice.DDD.Application.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(builder);
    }
}