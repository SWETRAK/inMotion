using System.Net.WebSockets;
using IMS.Shared.Domain.Entities.Other;
using IMS.Shared.Domain.Entities.Post;
using IMS.Shared.Domain.Entities.User;
using IMS.Shared.Domain.EntityPreferences;
using Microsoft.EntityFrameworkCore;

namespace IMS.Shared.Domain;

public class DomainDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfileVideo> UserProfileVideos { get; set; } 
    public DbSet<Provider> Providers { get; set; }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostVideo> PostVideos { get; set; }
    public DbSet<PostComment> PostComments { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Localization> Localizations { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
        new ProviderEntityConfiguration().Configure(modelBuilder.Entity<Provider>());
        new UserProfileVideoEntityConfiguration().Configure(modelBuilder.Entity<UserProfileVideo>());
        new LocalizationEntityConfiguration().Configure(modelBuilder.Entity<Localization>());
        new TagEntityConfiguartion().Configure(modelBuilder.Entity<Tag>());
        
        new PostEntityConfiguration().Configure(modelBuilder.Entity<Post>());
        new PostVideoEntityConfiguration().Configure(modelBuilder.Entity<PostVideo>());
        new PostCommentEntityConfiguration().Configure(modelBuilder.Entity<PostComment>());
    }
}