using IMS.Friends.Domain.Entities;
using IMS.Friends.Domain.EntityPreferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Friends.Domain;

public class ImsFriendsDbContext : DbContext
{
    public DbSet<Friendship> Friendships { get; set; }
    
    private readonly IConfiguration _configuration;

    public ImsFriendsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("WebApiDatabase"),
            x => x.MigrationsAssembly("IMS.Friends.API"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("friends");
        new FriendshipEntityConfiguration().Configure(modelBuilder.Entity<Friendship>());
    }
}