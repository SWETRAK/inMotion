using IMS.Post.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Post.Domain.EntityProperties.Post;

public class PostIterationEntityConfiguration: IEntityTypeConfiguration<PostIteration>
{
    public void Configure(EntityTypeBuilder<PostIteration> builder)
    {
        builder.ToTable("post_iterations");

        builder.HasIndex(p => p.Id);

        builder.Property(p => p.IterationName)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(p => p.StartDateTime)
            .IsRequired()
            .HasColumnName("start_date_time");
    }
}