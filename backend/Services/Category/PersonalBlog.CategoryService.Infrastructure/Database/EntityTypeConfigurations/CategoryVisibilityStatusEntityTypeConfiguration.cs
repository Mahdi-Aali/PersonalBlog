using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

namespace PersonalBlog.CategoryService.Infrastructure.Database.EntityTypeConfigurations;

public class CategoryVisibilityStatusEntityTypeConfiguration : IEntityTypeConfiguration<CategoryVisibilityStatus>
{
    public void Configure(EntityTypeBuilder<CategoryVisibilityStatus> builder)
    {
        builder.ToTable("CategoryVisibilityStatus", CategoryServiceDbContext.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(20);

        builder.HasData([CategoryVisibilityStatus.Enable, CategoryVisibilityStatus.Disable]);
    }
}