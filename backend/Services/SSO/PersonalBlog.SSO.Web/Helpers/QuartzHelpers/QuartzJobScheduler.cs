using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase;
using Quartz;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers;

public class QuartzJobScheduler
{
    public static IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> GetJobs()
    {
        return new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>()
        {
            {
                JobBuilder
                .Create()
                .WithIdentity("Seed data base", "database")
                .OfType(typeof(SeedDatabaseJob))
                .Build(),
                new List<ITrigger>()
                {
                    TriggerBuilder
                    .Create()
                    .WithIdentity("Seed data base job trigger")
                    .WithSimpleSchedule(cfg =>
                    {
                        cfg.WithRepeatCount(0);
                    })
                    .StartNow()
                    .Build()
                }
            }
        };
    }
}
