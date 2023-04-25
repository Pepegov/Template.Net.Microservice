using MicroserviceTemplate.DAL.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTemplate.DAL.Database;

public class ApplicationDbContext : DbContext
{
    public  DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}