using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Net.Microservice.DDD.Domain.Aggregate;

namespace Template.Net.Microservice.DDD.Application.Database.EntityConfiguration;

public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(o => o.Id);

        builder
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")  // Using a shadow property for communication
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(o => o.Taxes)
            .WithOne()
            .HasForeignKey("OrderId")  // Using a shadow property for communication
            .OnDelete(DeleteBehavior.Cascade);
    }
}