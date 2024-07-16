using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using System.Runtime.CompilerServices;

namespace PersonalBlog.SSO.Infrastructure.Database.EntityTypeConfigurations;

public class PersonalBlogRoleEntityTypeConfiguration : IEntityTypeConfiguration<PersonalBlogRole>
{
    public void Configure(EntityTypeBuilder<PersonalBlogRole> builder)
    {
        builder.Property(prop => prop.ConcurrencyStamp).IsConcurrencyToken();

        builder.Property(prop => prop.CreatedData).IsRequired();
        builder.Property(prop => prop.UpdatedDate).IsRequired(false);
    }
}
