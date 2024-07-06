using PersonalBlog.CategoryService.Api.Helpers.QuartzHelpers.Jobs;
using Quartz;

namespace PersonalBlog.CategoryService.Api.Helpers.QuartzHelpers;

public class QuartzJobScheduler
{
    public static IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> GetJobs()
    {
        return new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>()
        {
            {
                JobBuilder
                .Create()
                .WithIdentity("Seed data base job")
                .WithDescription("Used to apply pending migrations to database and seed data")
                .OfType<SeedDatabaseJob>()
                .Build(),
                new List<ITrigger>()
                {
                    TriggerBuilder
                    .Create()
                    .WithIdentity("seed data base job trigger")
                    .StartNow()
                    .WithSimpleSchedule(cfg =>
                    {
                        cfg.WithRepeatCount(0);
                    })
                    .Build()
                }
            }
        };
    }
}
