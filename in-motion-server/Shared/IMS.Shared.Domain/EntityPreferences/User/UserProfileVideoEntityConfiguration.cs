using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.User;

public class UserProfileVideoEntityConfiguration: IEntityTypeConfiguration<UserProfileVideo>
{
    public void Configure(EntityTypeBuilder<UserProfileVideo> builder)
    {
        builder.ToTable("user_profile_videos");

        builder.HasIndex(upv => upv.Id);

        builder.Property(upv => upv.AuthrorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(upv => upv.Filename)
            .HasColumnName("filename")
            .IsRequired();

        builder.Property(upv => upv.Description)
            .HasColumnName("description")
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