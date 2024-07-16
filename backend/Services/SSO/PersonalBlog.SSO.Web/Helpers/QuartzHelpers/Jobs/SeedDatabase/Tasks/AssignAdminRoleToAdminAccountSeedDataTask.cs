using Microsoft.AspNetCore.Identity;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Tasks;

public class AssignAdminRoleToAdminAccountSeedDataTask : ISeedDataTask
{
    public byte Pritory { get; set; } = 7;

    public async Task ExecuteAsync(IServiceProvider serviceProvider)
    {
        Logging.ILogger logger = serviceProvider.GetRequiredService<Logging.ILogger>();
        UserManager<PersonalBlogUser> userManager = serviceProvider.GetRequiredService<UserManager<PersonalBlogUser>>();

        await AssignAdminRoleToAdminAccountAsync(logger, userManager);
    }


    private async Task AssignAdminRoleToAdminAccountAsync(Logging.ILogger logger, UserManager<PersonalBlogUser> userManager)
    {
        await logger.LogInformation("Assigning admin role to admin account started.");

        PersonalBlogUser? adminUser = await userManager.FindByNameAsync("Admin");
        if (adminUser is not null)
        {
            string roleName = SeedDataTaskConstants.DefaultRoles[0];

            if (!await userManager.IsInRoleAsync(adminUser, roleName))
            {
                var result = await userManager.AddToRoleAsync(adminUser, roleName);
                if (result.Succeeded)
                {
                    await Task.CompletedTask;
                }
                else
                {
                    throw new InvalidOperationException($"Can't assign admin role to admin user. [ {string.Join(" - ", result.Errors.Select(error => error.Description))} ]");
                }
            }
        }
        else
        {
            throw new InvalidOperationException("There is no admin account in database to assing role on it.");
        }

        await logger.LogInformation("Assigning admin role to admin account ended.");
    }
}