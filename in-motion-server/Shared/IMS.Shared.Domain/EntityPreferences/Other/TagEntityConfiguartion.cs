using IMS.Shared.Domain.Entities.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Other;

public class TagEntityConfiguartion: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.HasIndex(t => t.Id);
        
        builder.Property(t => t.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(24)
            .IsRequired();

        builder.Property(t => t.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(t => t.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.HasOne(t => t.Author)
            .WithMany()
            .HasForeignKey(t => t.AuthorId);
    }
}