using IMS.User.Domain.Entities;
using IMS.User.Domain.EntityPreferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.User.Domain;

public class ImsUserDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<UserMetas> UserMetas { get; set; }
    public DbSet<UserProfileVideo> UserProfileVideos { get; set; }
    
    public ImsUserDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("WebApiDatabase"),
            x => x.MigrationsAssembly("IMS.User.API"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("user");
        new UserMetasEntityConfiguration().Configure(modelBuilder.Entity<UserMetas>());
        new UserProfileVideoEntityConfiguration().Configure(modelBuilder.Entity<UserProfileVideo>());
    }
}