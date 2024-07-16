using Microsoft.AspNetCore.Identity;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate.DomainEvents;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Tasks;

public class CreateDefaultRolesSeedDataTask : ISeedDataTask
{
    public byte Pritory { get; set; } = 8;

    public async Task ExecuteAsync(IServiceProvider serviceProvider)
    {
        Logging.ILogger logger = serviceProvider.GetRequiredService<Logging.ILogger>();
        RoleManager<PersonalBlogRole> roleManager = serviceProvider.GetRequiredService<RoleManager<PersonalBlogRole>>();

        await CreateDefaultRolesAsync(logger, roleManager);
    }


    private async Task CreateDefaultRolesAsync(Logging.ILogger logger, RoleManager<PersonalBlogRole> roleManager)
    {
        await logger.LogInformation("Creating default roles started.");

        foreach (string roleName in SeedDataTaskConstants.DefaultRoles)
        {
            if (await roleManager.FindByNameAsync(roleName) is null)
            {
                PersonalBlogRole role = new()
                {
                    Name = roleName,
                    CreatedData = DateTime.Now,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                role.AddDomainEvent(new NewRoleCreatedDomainEvents(role.Id, role.Name));

                await roleManager.CreateAsync(role);
            }
        }

        await logger.LogInformation("Creating default roles ended.");
    }
}
