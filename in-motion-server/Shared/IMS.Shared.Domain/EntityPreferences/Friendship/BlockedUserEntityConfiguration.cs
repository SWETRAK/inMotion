using IMS.Shared.Domain.Entities.Friendship;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Friendship;

public class BlockedUserEntityConfiguration: IEntityTypeConfiguration<BlockedUser>
{
    public void Configure(EntityTypeBuilder<BlockedUser> builder)
    {
        builder.ToTable("blocked_user");

        builder.HasIndex(bu => bu.Id);

        builder.Property(bu => bu.FirstUserId)
            .HasColumnName("first_user_id")
            .IsRequired();

        builder.Property(bu => bu.SecondUserId)
            .HasColumnName("second_user_id")
            .IsRequired();

        builder.Property(bu => bu.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.HasOne(bu => bu.FirstUser)
            .WithMany()
            .HasForeignKey(bu => bu.FirstUserId);

        builder.HasOne(bu => bu.SecondUser)
            .WithMany()
            .HasForeignKey(bu => bu.SecondUserId);
    }
}