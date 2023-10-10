using IMS.Post.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Post;

public class PostEntityConfiguration: IEntityTypeConfiguration<Entities.Post.Post>
{
    public void Configure(EntityTypeBuilder<Entities.Post.Post> builder)
    {
        builder.ToTable("posts");

        builder.HasIndex(p => p.Id);

        builder.Property(p => p.ExternalAuthorId)
            .HasColumnName("external_author_id")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(2048);

        builder.Property(p => p.Visibility)
            .HasColumnName("visibility")
            .IsRequired();

        builder.Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.LocalizationId)
            .HasColumnName("localization_id");

        builder.Property(p => p.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(p => p.LastModifiedDate)
            .HasColumnName("last_modified_date")
            .IsRequired();

        builder.Property(p => p.IterationId)
            .IsRequired()
            .HasColumnName("iteration_id");
        
        builder.HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity(join => join.ToTable("posts_tags_relations"));

        builder.HasMany(p => p.PostComments)
            .WithOne(pc => pc.Post)
            .HasForeignKey(pc => pc.PostId);

        builder.HasOne(p => p.Iteration)
            .WithMany(pi => pi.IterationPosts)
            .HasForeignKey(p => p.IterationId);
    }
}