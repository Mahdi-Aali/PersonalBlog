
using Microsoft.EntityFrameworkCore;
using PersonalBlog.SSO.Infrastructure.Database;
using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Tasks;

public class MigrateDatabaseSeedDataTask : ISeedDataTask
{
    public byte Pritory { get; set; } = 10;

    public virtual async Task ExecuteAsync(IServiceProvider serviceProvider)
    {
        Logging.ILogger logger = serviceProvider.GetRequiredService<Logging.ILogger>();
        SSODbContext context = serviceProvider.GetRequiredService<SSODbContext>();


        await MigrateAsync(logger, context);
    }


    private async Task MigrateAsync(Logging.ILogger logger, SSODbContext context)
    {
        await logger.LogInformation("Database migration started.");

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }

        await logger.LogInformation("Database migration ended.");
    }
}
