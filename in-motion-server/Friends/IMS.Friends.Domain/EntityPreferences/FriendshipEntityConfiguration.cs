using IMS.Friends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Friends.Domain.EntityPreferences;

public class FriendshipEntityConfiguration: IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ToTable("friendships");

        builder.HasIndex(f => f.Id)
            .IncludeProperties(p =>
                new { p.SecondUserId, p.FirstUserId }
             );
        
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
    }
}