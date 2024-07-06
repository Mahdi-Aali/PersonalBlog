using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using System.Runtime.CompilerServices;

namespace PersonalBlog.CategoryService.Infrastructure.Database.EntityTypeConfigurations;

public sealed class CategoryEntityTypeConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", CategoryServiceDbContext.SCHEMA, cfg =>
        {
            cfg.IsTemporal(tempCfg =>
            {
                tempCfg.UseHistoryTable($"Archive_{cfg.Name}", CategoryServiceDbContext.SCHEMA);
                tempCfg.HasPeriodStart("FromDate");
                tempCfg.HasPeriodEnd("ToDate");
            });
        });

        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id).ValueGeneratedNever();
        builder.Property(prop => prop.ConcurencyToken).IsConcurrencyToken(true);
        builder.Property(prop => prop.Title).IsRequired().HasMaxLength(50);
        builder.Property(prop => prop.Description).IsRequired().HasMaxLength(350);
        builder.Property(prop => prop.CreatedDate).IsRequired();
        builder.Property(prop => prop.UpdatedDate).IsRequired(false);
        builder.Property(prop => prop.CategoryVisibilityStatusId).IsRequired();


        builder.HasOne(x => x.CategoryVisibilityStatus)
            .WithOne()
            .HasForeignKey<Category>(fk => fk.CategoryVisibilityStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
