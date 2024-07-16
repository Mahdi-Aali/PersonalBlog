
using Microsoft.AspNetCore.Identity;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate.DomainEvents;
using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Tasks;

public class CreateAdminAccountSeedDataTask : ISeedDataTask
{
    public byte Pritory { get; set; } = 9;

    public virtual async Task ExecuteAsync(IServiceProvider serviceProvider)
    {
        Logging.ILogger logger = serviceProvider.GetRequiredService<Logging.ILogger>();
        UserManager<PersonalBlogUser> userManager = serviceProvider.GetRequiredService<UserManager<PersonalBlogUser>>();

        await CreateAdminAccountAsync(logger, userManager);
    }


    private async Task CreateAdminAccountAsync(Logging.ILogger logger, UserManager<PersonalBlogUser> userManager)
    {
        await logger.LogInformation("Creating Admin account started.");

        if (await userManager.FindByNameAsync("Admin") is null)
        {

            PersonalBlogUser adminUser = new()
            {
                AccessFailedCount = 3,
                CreatedData = DateTime.Now,
                Email = "Admin@MahdiPersonalBlog.info",
                UserName = "Admin",
                PhoneNumber = "+989058785110",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserAccountStatusId = UserAccountStatus.Active.Id,
                LockoutEnabled = true,
                TwoFactorEnabled = true
            };

            adminUser.AddDomainEvent(new NewUserCreatedDomainEvent(adminUser.Id, adminUser.UserName!));

            var result = await userManager.CreateAsync(adminUser, "P3._50n@l31O9```");
            if (result.Succeeded)
            {
                await Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException($"Can't create admin user account, {string.Join(" - ", result.Errors.Select(s => s.Description))}");
            }
        }

        await logger.LogInformation("Creating admin account ended.");
    }
}
