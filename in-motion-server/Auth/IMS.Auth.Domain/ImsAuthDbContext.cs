using IMS.Auth.Domain.Entities;
using IMS.Auth.Domain.EntityPreferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Auth.Domain;

public class ImsAuthDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Provider> Providers { get; set; }
    
    private readonly IConfiguration _configuration;
    
    public ImsAuthDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"), x =>
        {
            x.MigrationsAssembly("IMS.Auth.API");
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auth");
        new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
        new ProviderEntityConfiguration().Configure(modelBuilder.Entity<Provider>());
    }
}