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
        
        builder.Property(u => u.Bio)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(u => u.Nickname)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.ProfileVideoId)
            .HasColumnName("profile_video_id");
        
        builder.HasOne(u => u.ProfileVideo)
            .WithOne(upv => upv.Author)
            .HasForeignKey<Entities.User.User>(u => u.ProfileVideoId)
            .HasPrincipalKey<UserProfileVideo>(upv => upv.AuthrorId);
    }
}