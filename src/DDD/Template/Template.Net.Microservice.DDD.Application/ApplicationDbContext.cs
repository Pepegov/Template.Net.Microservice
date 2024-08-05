using Microsoft.EntityFrameworkCore;
using Template.Net.Microservice.DDD.Domain.Aggregate;
using Template.Net.Microservice.DDD.Domain.Entity;

namespace Template.Net.Microservice.DDD.Application;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Order>()
            .HasKey(o => o.Id);

        builder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")  // Using a shadow property for communication
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasMany(o => o.Taxes)
            .WithOne()
            .HasForeignKey("OrderId")  // Using a shadow property for communication
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Product>()
            .Property<Guid>("OrderId");  // Defining a shadow property

        builder.Entity<Tax>()
            .Property<Guid>("OrderId");  // Defining a shadow property
    }
}