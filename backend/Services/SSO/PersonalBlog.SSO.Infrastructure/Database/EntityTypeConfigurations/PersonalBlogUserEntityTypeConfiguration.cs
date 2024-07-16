using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

namespace PersonalBlog.SSO.Infrastructure.Database.EntityTypeConfigurations;

public class PersonalBlogUserEntityTypeConfiguration : IEntityTypeConfiguration<PersonalBlogUser>
{
    public void Configure(EntityTypeBuilder<PersonalBlogUser> builder)
    {
        builder.Property(prop => prop.ConcurrencyStamp).IsConcurrencyToken();
        builder.Property(prop => prop.UpdatedDate).IsRequired(false);
        builder.Property(prop => prop.CreatedData).IsRequired();
        builder.Property(prop => prop.UserAccountStatusId).IsRequired();

        builder.HasOne(x => x.UserAccountStatus)
            .WithOne()
            .HasForeignKey<PersonalBlogUser>(fk => fk.UserAccountStatusId);
    }
}
