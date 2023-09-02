
using IMS.Shared.Domain.Entities.Other;
using IMS.Shared.Domain.Entities.Post;
using IMS.Shared.Domain.EntityPreferences.Other;
using IMS.Shared.Domain.EntityPreferences.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Shared.Domain;
 
/*
To create migrations execute command 
- dotnet ef migrations add Initial --context DomainDbContext 

To generate sql script execute command 
- dotnet ef migrations script --context DomainDbContext --output "../../../in-motion-database/scripts/ims-db-09.07.2023.sql"   
*/

/// <summary>
/// Dont use this class for development instead use ImsDbContext
/// </summary>
public class DomainDbContext: DbContext
{
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
        new LocalizationEntityConfiguration().Configure(modelBuilder.Entity<Localization>());
        new TagEntityConfiguartion().Configure(modelBuilder.Entity<Tag>());
        
        new PostEntityConfiguration().Configure(modelBuilder.Entity<Post>());
        new PostVideoEntityConfiguration().Configure(modelBuilder.Entity<PostVideo>());
        new PostCommentEntityConfiguration().Configure(modelBuilder.Entity<PostComment>());
        new PostReactionEntityConfiguration().Configure(modelBuilder.Entity<PostReaction>());
        new PostCommentReactionEntityConfiguration().Configure(modelBuilder.Entity<PostCommentReaction>());
    }
}