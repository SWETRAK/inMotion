using IMS.Post.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Post;

public class PostVideoEntityConfiguration: IEntityTypeConfiguration<PostVideo>
{
    public void Configure(EntityTypeBuilder<PostVideo> builder)
    {
        builder.ToTable("post_videos");

        builder.HasIndex(pv => pv.Id);

        builder.Property(pv => pv.ExternalAuthorId)
            .HasColumnName("external_author_id")
            .IsRequired();

        builder.Property(pv => pv.Filename)
            .HasColumnName("filename")
            .IsRequired();

        builder.Property(pv => pv.ContentType)
            .HasColumnName("content_type")
            .IsRequired();

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
        
        builder.Property(pv => pv.BucketLocation)
            .HasColumnName("bucket_location")
            .IsRequired();

        builder.Property(pv => pv.BucketName)
            .HasColumnName("bucket_name")
            .IsRequired();
    }
}