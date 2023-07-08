using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences.User;

public class UserEntityConfiguration: IEntityTypeConfiguration<Entities.User.User>
{
    public void Configure(EntityTypeBuilder<Entities.User.User> builder)
    {
        builder.ToTable("users");

        builder.HasIndex(u => u.Id);

        builder.Property(u => u.Email)
            .HasColumnName("email`")
            .IsRequired();

        builder.Property(u => u.ActivationToken)
            .HasColumnName("activation_token");

        builder.Property(u => u.ConfirmedAccount)
            .IsRequired();

        builder.Property(u => u.Bio)
            .HasColumnName("bio")
            .HasMaxLength(1024);

        builder.Property(u => u.Nickname)
            .HasColumnName("nickname")
            .IsRequired()
            .HasMaxLength(24);

        builder.Property(u => u.ProfileVideoId)
            .HasColumnName("profile_video_id");

        builder.Property(u => u.HashedPassword)
            .HasColumnName("hashed_password");

        builder.HasOne(u => u.ProfileVideo)
            .WithOne(upv => upv.Author)
            .HasForeignKey<Entities.User.User>(u => u.ProfileVideoId)
            .HasPrincipalKey<UserProfileVideo>(upv => upv.AuthrorId);
    }
}