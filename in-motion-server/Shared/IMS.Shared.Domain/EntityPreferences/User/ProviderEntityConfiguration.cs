using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.User;

public class ProviderEntityConfiguration: IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("providers");
        
        builder.HasIndex(p => p.Id);
        
        builder.Property(p => p.AuthKey)
            .HasColumnName("auth_key")
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.HasOne(u => u.User)
            .WithMany(p => p.Providers)
            .HasForeignKey(p => p.UserId);
    }
}