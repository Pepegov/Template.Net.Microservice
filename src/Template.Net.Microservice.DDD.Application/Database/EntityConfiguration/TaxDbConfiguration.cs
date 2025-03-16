using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Net.Microservice.DDD.Domain.Entity;

namespace Template.Net.Microservice.DDD.Application.Database.EntityConfiguration;

public class TaxDbConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder
            .Property<Guid>("OrderId");  // Defining a shadow property
    }
}