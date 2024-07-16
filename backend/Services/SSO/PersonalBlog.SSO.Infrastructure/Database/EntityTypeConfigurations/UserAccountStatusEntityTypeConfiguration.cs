using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

namespace PersonalBlog.SSO.Infrastructure.Database.EntityTypeConfigurations;

public class UserAccountStatusEntityTypeConfiguration : IEntityTypeConfiguration<UserAccountStatus>
{
    public void Configure(EntityTypeBuilder<UserAccountStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(prop => prop.Id).ValueGeneratedNever();

        builder.Property(prop => prop.Title).IsRequired().HasMaxLength(150);

        builder.HasData(UserAccountStatus.AccountStatuses);
    }
}
