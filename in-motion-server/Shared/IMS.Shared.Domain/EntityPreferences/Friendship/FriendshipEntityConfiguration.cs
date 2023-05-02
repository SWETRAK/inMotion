using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Friendship;

public class FriendshipEntityConfiguration: IEntityTypeConfiguration<Entities.Friendship.Friendship>
{
    public void Configure(EntityTypeBuilder<Entities.Friendship.Friendship> builder)
    {
        builder.ToTable("friendships");

        builder.HasIndex(f => f.Id);

        builder.Property(f => f.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(f => f.FirstUserId)
            .HasColumnName("first_user_id")
            .IsRequired();

        builder.Property(f => f.SecondUserId)
            .HasColumnName("second_user_id")
            .IsRequired();

        builder.Property(f => f.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(f => f.LastModificationDate)
            .HasColumnName("last_modification_date")
            .IsRequired();

        builder.HasOne(f => f.FirstUser)
            .WithMany()
            .HasForeignKey(f => f.FirstUserId);

        builder.HasOne(f => f.SecondUser)
            .WithMany()
            .HasForeignKey(f => f.SecondUserId);
    }
}