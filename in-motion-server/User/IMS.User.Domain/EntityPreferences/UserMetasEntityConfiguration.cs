using IMS.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.User.Domain.EntityPreferences;

public class UserMetasEntityConfiguration: IEntityTypeConfiguration<UserMetas>
{
    public void Configure(EntityTypeBuilder<UserMetas> builder)
    {
        builder.ToTable("user_metas");

        builder.HasIndex(upv => upv.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.UserExternalId)
            .IsRequired();
        
        builder.Property(u => u.Bio)
            .HasColumnName("bio")
            .HasMaxLength(1024);

        builder.Property(u => u.ProfileVideoId)
            .HasColumnName("profile_video_id");

        builder.HasOne(u => u.ProfileVideo)
            .WithOne(upv => upv.UserMetas)
            .HasForeignKey<UserMetas>(u => u.ProfileVideoId)
            .HasPrincipalKey<UserProfileVideo>(upv => upv.UserMetasId);
    }
}