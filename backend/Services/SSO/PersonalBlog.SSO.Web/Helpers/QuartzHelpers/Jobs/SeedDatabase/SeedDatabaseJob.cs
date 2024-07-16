using PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;
using Quartz;

namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase;

public class SeedDatabaseJob : IJob
{
    private readonly Logging.ILogger _logger;
    private readonly IServiceProvider _serviceProvider;


    public SeedDatabaseJob(Logging.ILogger logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider.CreateAsyncScope().ServiceProvider;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            IEnumerable<ISeedDataTask> seedDataTasks = 
             GetType()
            .Assembly
            .ExportedTypes
            .Where(type => typeof(ISeedDataTask).IsAssignableFrom(type) && type != typeof(ISeedDataTask))
            .Select(_ => (ISeedDataTask) Activator.CreateInstance(_)!)
            .OrderByDescending(item => item.Pritory);


            foreach(ISeedDataTask task in seedDataTasks)
            {
                await task.ExecuteAsync(_serviceProvider);
            }
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Something went wrong when initializing the app for start.");
            await Task.CompletedTask;
        }
    }
}