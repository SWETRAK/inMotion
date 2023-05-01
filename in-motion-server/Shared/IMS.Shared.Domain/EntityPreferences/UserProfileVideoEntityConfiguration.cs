using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences;

public class UserProfileVideoEntityConfiguration: IEntityTypeConfiguration<UserProfileVideo>
{
    public void Configure(EntityTypeBuilder<UserProfileVideo> builder)
    {
        builder.ToTable("user_profile_video");

        builder.Property(upv => upv.AuthrorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(upv => upv.Filename)
            .IsRequired();

        builder.Property(upv => upv.Description)
            .HasMaxLength(2048);

        builder.Property(upv => upv.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(upv => upv.LastEditionDate)
            .HasColumnName("last_edition_name")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);
    }
}