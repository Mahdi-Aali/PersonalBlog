using Microsoft.AspNetCore.Identity;
using Logging = PersonalBlog.BuildingBlocks.Logging.Contracts;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using Quartz;
using PersonalBlog.SSO.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate.DomainEvents;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate.DomainEvents;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs;

public class SeedDatabaseJob : IJob
{
    private readonly SSODbContext _context;
    private readonly UserManager<PersonalBlogUser> _userManager;
    private readonly RoleManager<PersonalBlogRole> _roleManager;
    private readonly Logging::ILogger _logger;
    private PersonalBlogUser _adminUser;
    private readonly string _defaultRolePrefix;
    private readonly string[] _defaultRoles;


    public SeedDatabaseJob(
        SSODbContext context,
        UserManager<PersonalBlogUser> userManager,
        RoleManager<PersonalBlogRole> roleManager,
        Logging.ILogger logger
        )
    {
        _adminUser = new()
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

        _defaultRolePrefix = "sys";

        string[] defaultRoles = ["Admin", "User"];

        _defaultRoles = defaultRoles.Select(s => $"{_defaultRolePrefix}{s}").ToArray();
    }


    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await MigrateAsync();

            await CreateAdminAccount();

            await CreateDefaultRoles();

        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Something went wrong when initializing the app for start.");
            await Task.CompletedTask;
        }
    }

    private async Task MigrateAsync()
    {
        await _logger.LogInformation("Database migration started.");

        if ((await _context.Database.GetPendingMigrationsAsync()).Any())
        {
            await _context.Database.MigrateAsync();
        }

        await _logger.LogInformation("Database migration ended.");
    }

    private async Task CreateAdminAccount()
    {
        await _logger.LogInformation("Creating Admin account started.");

        if (await _userManager.FindByNameAsync("Admin") is null)
        {
            _adminUser.AddDomainEvent(new NewUserCreatedDomainEvent(_adminUser.Id, _adminUser.UserName!));

            var result = await _userManager.CreateAsync(_adminUser, "P3._50n@l31O9```");
            if (result.Succeeded)
            {
                await Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException($"Can't create admin user account, {string.Join(" - ", result.Errors.Select(s => s.Description))}");
            }
        }

        await _logger.LogInformation("Creating admin account ended.");
    }

    private async Task CreateDefaultRoles()
    {
        await _logger.LogInformation("Creating default roles started.");

        foreach(string roleName in _defaultRoles)
        {
            if (await _roleManager.FindByNameAsync(roleName) is null)
            {
                PersonalBlogRole role = new()
                {
                    Name = roleName,
                    CreatedData = DateTime.Now,
                    OwnerId = _adminUser.Id
                };

                role.AddDomainEvent(new NewRoleCreatedDomainEvents(role.Id, role.Name, role.OwnerId));

                await _roleManager.CreateAsync(role);
            }
        }

        await _logger.LogInformation("Creating default roles ended.");
    }


}