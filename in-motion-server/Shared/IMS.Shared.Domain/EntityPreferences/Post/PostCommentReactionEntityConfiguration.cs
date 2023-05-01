using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Post;

public class PostCommentReactionEntityConfiguration: IEntityTypeConfiguration<PostCommentReaction>
{
    public void Configure(EntityTypeBuilder<PostCommentReaction> builder)
    {
        builder.ToTable("post_comment_reaction");

        builder.HasIndex(pcr => pcr.Id);

        builder.Property(pcr => pcr.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(pcr => pcr.Emoji)
            .HasColumnName("emoji")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(pcr => pcr.CreationDate)
            .HasColumnName("creation_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(pcr => pcr.LastModificationDate)
            .HasColumnName("last_modification_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(pcr => pcr.PostCommentId)
            .HasColumnName("post_comment_id")
            .IsRequired();

        builder.HasOne(pcr => pcr.Author)
            .WithMany()
            .HasForeignKey(pcr => pcr.AuthorId);

        builder.HasOne(pcr => pcr.PostComment)
            .WithMany()
            .HasForeignKey(pcr => pcr.PostCommentId);
    }
}