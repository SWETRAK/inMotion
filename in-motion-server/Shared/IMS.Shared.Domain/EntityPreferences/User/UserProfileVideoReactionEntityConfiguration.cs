using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.User;

public class UserProfileVideoReactionEntityConfiguration: IEntityTypeConfiguration<UserProfileVideoReaction>
{
    public void Configure(EntityTypeBuilder<UserProfileVideoReaction> builder)
    {
        builder.ToTable("user_profile_video_reactions");
        
        builder.HasIndex(upvr => upvr.Id);

        builder.Property(upvr => upvr.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(upvr => upvr.Emoji)
            .HasColumnName("emoji")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(upvr => upvr.CreationDate)
            .HasColumnName("creation_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(upvr => upvr.LastModificationDate)
            .HasColumnName("last_modification_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        
        builder.Property(upvr => upvr.UserProfileVideoId)
            .HasColumnName("user_profile_video_id")
            .IsRequired();

        builder.HasOne(upvr => upvr.Author)
            .WithMany()
            .HasForeignKey(upvr => upvr.AuthorId);

        builder.HasOne(upvr => upvr.UserProfileVideo)
            .WithMany(upv => upv.UserProfileVideoReactions)
            .HasForeignKey(upvr => upvr.UserProfileVideoId);
    }
}