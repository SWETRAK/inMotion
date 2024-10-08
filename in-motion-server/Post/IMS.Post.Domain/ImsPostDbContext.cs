using IMS.Post.Domain.Entities.Post;
using IMS.Post.Domain.EntityProperties.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Post.Domain;

public class ImsPostDbContext: DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<Entities.Post.Post> Posts { get; set; }
    public DbSet<PostVideo> PostVideos { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<PostIteration> PostIterations { get; set; }

    public ImsPostDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("WebApiDatabase"),
            x => x.MigrationsAssembly("IMS.Post.API"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("post");
        
        new PostEntityConfiguration().Configure(modelBuilder.Entity<Entities.Post.Post>());
        new PostVideoEntityConfiguration().Configure(modelBuilder.Entity<PostVideo>());
        new PostCommentEntityConfiguration().Configure(modelBuilder.Entity<PostComment>());
        new PostReactionEntityConfiguration().Configure(modelBuilder.Entity<PostReaction>());
        
        new PostIterationEntityConfiguration().Configure(modelBuilder.Entity<PostIteration>());
    }
}
