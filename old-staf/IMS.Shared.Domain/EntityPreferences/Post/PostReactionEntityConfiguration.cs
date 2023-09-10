using IMS.Post.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Post;

public class PostReactionEntityConfiguration: IEntityTypeConfiguration<PostReaction>
{
    public void Configure(EntityTypeBuilder<PostReaction> builder)
    {
        builder.ToTable("post_reactions");

        builder.HasIndex(pr => pr.Id);

        builder.Property(pr => pr.ExternalAuthorId)
            .HasColumnName("external_author_id")
            .IsRequired();

        builder.Property(pr => pr.Emoji)
            .HasColumnName("emoji")
            .IsRequired();

        builder.Property(pr => pr.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(pr => pr.LastModificationDate)
            .HasColumnName("last_modification_date")
            .IsRequired();

        builder.Property(pr => pr.PostId)
            .HasColumnName("post_id")
            .IsRequired();

        builder.HasOne(pr => pr.Post)
            .WithMany(p => p.PostReactions)
            .HasForeignKey(pr => pr.PostId);
    }
}