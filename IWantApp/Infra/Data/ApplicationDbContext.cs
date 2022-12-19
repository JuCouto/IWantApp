using Flunt.Notifications;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; } 

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // chamando a classe "pai" para adiconar a modelagem do Identity.
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
