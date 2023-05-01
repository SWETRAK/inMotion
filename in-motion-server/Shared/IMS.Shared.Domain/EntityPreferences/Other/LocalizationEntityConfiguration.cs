using IMS.Shared.Domain.Entities.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.Other;

public class LocalizationEntityConfiguration: IEntityTypeConfiguration<Localization>
{
    public void Configure(EntityTypeBuilder<Localization> builder)
    {
        builder.ToTable("localizations");

        builder.HasIndex(l => l.Id);
        
        builder.Property(l => l.Name)
            .HasColumnName("name")
            .HasMaxLength(512);

        builder.Property(l => l.Latitude)
            .HasColumnName("latitude")
            .IsRequired();

        builder.Property(l => l.Longitude)
            .HasColumnName("longitude")
            .IsRequired();
    }
}