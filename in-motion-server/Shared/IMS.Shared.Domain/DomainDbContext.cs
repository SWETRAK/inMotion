using IMS.Shared.Domain.Entities.Friendship;
using IMS.Shared.Domain.Entities.Other;
using IMS.Shared.Domain.Entities.Post;
using IMS.Shared.Domain.Entities.User;
using IMS.Shared.Domain.EntityPreferences.Friendship;
using IMS.Shared.Domain.EntityPreferences.Other;
using IMS.Shared.Domain.EntityPreferences.Post;
using IMS.Shared.Domain.EntityPreferences.User;
using Microsoft.EntityFrameworkCore;

namespace IMS.Shared.Domain;
 
/*
To create migrations execute command 
- dotnet ef migrations add Initial --context DomainDbContext 

To generate sql script execute command 
- dotnet ef migrations script --output "version1.sql"      
*/

/// <summary>
/// Dont use this class for development instead use ImsDbContext
/// </summary>
public class DomainDbContext: DbContext
{ 
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfileVideo> UserProfileVideos { get; set; } 
    public DbSet<Provider> Providers { get; set; }

    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostVideo> PostVideos { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<PostCommentReaction> PostCommentReactions { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Localization> Localizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
        new ProviderEntityConfiguration().Configure(modelBuilder.Entity<Provider>());
        new UserProfileVideoEntityConfiguration().Configure(modelBuilder.Entity<UserProfileVideo>());

        new LocalizationEntityConfiguration().Configure(modelBuilder.Entity<Localization>());
        new TagEntityConfiguartion().Configure(modelBuilder.Entity<Tag>());
        
        new FriendshipEntityConfiguration().Configure(modelBuilder.Entity<Friendship>());

        new PostEntityConfiguration().Configure(modelBuilder.Entity<Post>());
        new PostVideoEntityConfiguration().Configure(modelBuilder.Entity<PostVideo>());
        new PostCommentEntityConfiguration().Configure(modelBuilder.Entity<PostComment>());
        new PostReactionEntityConfiguration().Configure(modelBuilder.Entity<PostReaction>());
        new PostCommentReactionEntityConfiguration().Configure(modelBuilder.Entity<PostCommentReaction>());
    }
}