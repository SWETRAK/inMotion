using IMS.Post.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Post;

public class PostCommentReactionEntityConfiguration: IEntityTypeConfiguration<PostCommentReaction>
{
    public void Configure(EntityTypeBuilder<PostCommentReaction> builder)
    {
        builder.ToTable("post_comment_reaction");

        builder.HasIndex(pcr => pcr.Id);

        builder.Property(pcr => pcr.ExternalAuthorId)
            .HasColumnName("external_author_id")
            .IsRequired();

        builder.Property(pcr => pcr.Emoji)
            .HasColumnName("emoji")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(pcr => pcr.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(pcr => pcr.LastModificationDate)
            .HasColumnName("last_modification_date")
            .IsRequired();

        builder.Property(pcr => pcr.PostCommentId)
            .HasColumnName("post_comment_id")
            .IsRequired();

        builder.HasOne(pcr => pcr.PostComment)
            .WithMany()
            .HasForeignKey(pcr => pcr.PostCommentId);
    }
}