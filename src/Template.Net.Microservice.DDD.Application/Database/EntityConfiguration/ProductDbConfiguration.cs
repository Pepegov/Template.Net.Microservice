using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Net.Microservice.DDD.Domain.Entity;

namespace Template.Net.Microservice.DDD.Application.Database.EntityConfiguration;

public class ProductDbConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property<Guid>("OrderId");  // Defining a shadow property
    }
}