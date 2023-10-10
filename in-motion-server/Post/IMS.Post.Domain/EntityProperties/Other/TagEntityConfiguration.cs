using IMS.Post.Domain.Entities.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Other;

public class TagEntityConfiguration: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.HasIndex(t => t.Id);
        
        builder.Property(t => t.ExternalAuthorId)
            .HasColumnName("external_author_id")
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(24)
            .IsRequired();

        builder.Property(t => t.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();
    }
}