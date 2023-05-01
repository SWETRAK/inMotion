using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences;

public class PostVideoEntityConfiguration: IEntityTypeConfiguration<PostVideo>
{
    public void Configure(EntityTypeBuilder<PostVideo> builder)
    {
        builder.ToTable("post_videos");

        builder.Property(pv => pv.AuthrorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(pv => pv.Filename)
            .IsRequired();

        builder.Property(pv => pv.Description)
            .HasMaxLength(2048);

        builder.Property(pv => pv.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(pv => pv.LastEditionDate)
            .HasColumnName("last_edition_name")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(pv => pv.Type)
            .IsRequired();

        builder.Property(pv => pv.PostFrontId)
            .HasColumnName("post_front_id");

        builder.Property(pv => pv.PostRearId)
            .HasColumnName("post_rear_id");
    }
}