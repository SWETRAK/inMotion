using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Post;

public class PostVideoEntityConfiguration: IEntityTypeConfiguration<PostVideo>
{
    public void Configure(EntityTypeBuilder<PostVideo> builder)
    {
        builder.ToTable("post_videos");

        builder.HasIndex(pv => pv.Id);

        builder.Property(pv => pv.AuthrorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(pv => pv.Filename)
            .HasColumnName("filename")
            .IsRequired();

        builder.Property(pv => pv.Description)
            .HasColumnName("description")
            .HasMaxLength(2048);

        builder.Property(pv => pv.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Property(pv => pv.LastEditionDate)
            .HasColumnName("last_edition_name")
            .IsRequired();

        builder.Property(pv => pv.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(pv => pv.PostFrontId)
            .HasColumnName("post_front_id")
            .IsRequired();

        builder.Property(pv => pv.PostRearId)
            .HasColumnName("post_rear_id")
            .IsRequired();
    }
}