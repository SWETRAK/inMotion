using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Auth.Domain.EntityPreferences;

public class UserEntityConfiguration: IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        builder.ToTable("users");

        builder.HasIndex(u => u.Id);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(u => u.ActivationToken)
            .HasColumnName("activation_token");

        builder.Property(u => u.ConfirmedAccount)
            .IsRequired();

        builder.Property(u => u.Nickname)
            .HasColumnName("nickname")
            .IsRequired()
            .HasMaxLength(24);

        builder.Property(u => u.HashedPassword)
            .HasColumnName("hashed_password");
    }
}