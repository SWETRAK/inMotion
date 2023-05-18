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

        builder.Property(upv => upv.ContentType)
            .HasColumnName("content_type")
            .IsRequired();

        builder.Property(upv => upv.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(upv => upv.BucketLocation)
            .HasColumnName("bucket_location")
            .IsRequired();

        builder.Property(upv => upv.BucketName)
            .HasColumnName("bucket_name")
            .IsRequired();

        builder.Property(upv => upv.LastEditionDate)
            .HasColumnName("last_edition_name")
            .IsRequired();
    }
}