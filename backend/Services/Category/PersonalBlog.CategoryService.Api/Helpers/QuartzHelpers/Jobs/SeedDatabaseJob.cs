using Microsoft.EntityFrameworkCore;
using PersonalBlog.BuildingBlocks.Extenssions.Tasks;
using PersonalBlog.CategoryService.Infrastructure.Database;
using Quartz;

namespace PersonalBlog.CategoryService.Api.Helpers.QuartzHelpers.Jobs;

public class SeedDatabaseJob : IJob
{
    private readonly CategoryServiceDbContext _context;
    private readonly Logging.ILogger _logger;

    public SeedDatabaseJob(CategoryServiceDbContext context, Logging.ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await ApplyPendingMigrations().WithTimeout(TimeSpan.FromMinutes(3));
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Error when seeding database.");
        }
    }

    private async Task ApplyPendingMigrations()
    {
        if ((await _context.Database.GetPendingMigrationsAsync()).Any())
        {
            await _context.Database.MigrateAsync();
        }
    }
}
