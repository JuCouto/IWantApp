using Flunt.Notifications;
using IWantApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; } 

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();

        builder.Entity<Product>()
           .Property(p => p.Name).IsRequired();

        builder.Entity<Product>()
           .Property(p => p.Description).HasMaxLength(300);

        builder.Entity<Category>()
           .Property(c => c.Name).IsRequired();
    }

    // Configurar convenção, toda propriedade string terá no máximo 100 caracteres.
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
