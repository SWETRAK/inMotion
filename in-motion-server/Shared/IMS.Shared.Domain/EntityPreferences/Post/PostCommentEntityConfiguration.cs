using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Post;

public class PostCommentEntityConfiguration: IEntityTypeConfiguration<PostBaseComment>
{
    public void Configure(EntityTypeBuilder<PostBaseComment> builder)
    {
        builder.ToTable("post_comments");

        builder.HasIndex(pc => pc.Id);

        builder.Property(pc => pc.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(pc => pc.Content)
            .HasColumnName("content")
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(pc => pc.CreationDate)
            .HasColumnName("creation_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(pc => pc.LastModifiedDate)
            .HasColumnName("last_modified_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }
}