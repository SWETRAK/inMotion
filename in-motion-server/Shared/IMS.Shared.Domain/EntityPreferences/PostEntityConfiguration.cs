using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences;

public class PostEntityConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("post");

        builder.Property(p => p.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(2048);

        builder.Property(p => p.Title)
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

        builder.HasOne(p => p.FrontVideo)
            .WithOne(p => p.PostFront)
            .HasForeignKey<Post>(p => p.FrontVideoId)
            .HasPrincipalKey<PostVideo>(pv => pv.PostFrontId);

        builder.HasOne(p => p.RearVideo)
            .WithOne(p => p.PostRear)
            .HasForeignKey<Post>(p => p.RearVideoId)
            .HasPrincipalKey<PostVideo>(pv => pv.PostRearId);

        builder.HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId);

        builder.HasMany(p => p.Tags)
            .WithMany();

        builder.HasMany(p => p.PostComments)
            .WithOne(pc => pc.Post)
            .HasForeignKey(pc => pc.PostId);
    }
}