using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Post;

public class PostReactionEntityConfiguration: IEntityTypeConfiguration<PostReaction>
{
    public void Configure(EntityTypeBuilder<PostReaction> builder)
    {
        builder.ToTable("post_reactions");

        builder.HasIndex(pr => pr.Id);

        builder.Property(pr => pr.AuthorId)
            .HasColumnName("author_id")
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

        builder.HasOne(pr => pr.Author)
            .WithMany()
            .HasForeignKey(pr => pr.AuthorId);

        builder.HasOne(pr => pr.Post)
            .WithMany(p => p.PostReactions)
            .HasForeignKey(pr => pr.PostId);
    }
}