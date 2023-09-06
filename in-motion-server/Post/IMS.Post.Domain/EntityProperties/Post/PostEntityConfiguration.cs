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

        builder.Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.LocalizationId)
            .HasColumnName("localization_id");

        builder.Property(p => p.FrontVideoId)
            .HasColumnName("front_video_id")
            .IsRequired();

        builder.Property(p => p.RearVideoId)
            .HasColumnName("rear_video_id")
            .IsRequired();

        builder.Property(p => p.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(p => p.LastModifiedDate)
            .HasColumnName("last_modified_date")
            .IsRequired();

        builder.HasOne(p => p.FrontVideo)
            .WithOne(p => p.PostFront)
            .HasForeignKey<Entities.Post.Post>(p => p.FrontVideoId)
            .HasPrincipalKey<PostVideo>(pv => pv.PostFrontId);

        builder.HasOne(p => p.RearVideo)
            .WithOne(p => p.PostRear)
            .HasForeignKey<Entities.Post.Post>(p => p.RearVideoId)
            .HasPrincipalKey<PostVideo>(pv => pv.PostRearId);
        
        builder.HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity(join => join.ToTable("posts_tags_relations"));

        builder.HasMany(p => p.PostComments)
            .WithOne(pc => pc.Post)
            .HasForeignKey(pc => pc.PostId);
    }
}