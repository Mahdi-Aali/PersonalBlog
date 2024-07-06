using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.CategoryService.Api.Helpers.QuartzHelpers;
using Quartz;
using Serilog;
namespace PersonalBlog.CategoryService.Api.StartupConfiguration;

public abstract class ApiStartup : StartupBase
{
    public sealed override void ConfigureService(WebApplicationBuilder builder)
    {
        base.ConfigureService(builder);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.Elasticsearch([new Uri(ProjectConfigurations.GetConnectionString("elastic-search")!)])
            .CreateLogger();

        IServiceCollection services = builder.Services;
        ResolveAssembliesDependencies(services);
    }

    public sealed override async Task Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSerilogRequestLogging();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseCors(ProjectConfigurations["CorsName"]!);

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapDefaultControllerRoute();
        app.MapGet("/", async (HttpContext context) =>
        {
            await context.Response.WriteAsync("Category services");
        });
        app.MapGet("/hc", async (HttpContext context) =>
        {
            await context.Response.WriteAsync("Healthy...");
        });

        await ScheduleCronJobs(app);

        await base.Configure(app, env);
    }


    private IServiceCollection ResolveAssembliesDependencies(IServiceCollection services)
    {
        IEnumerable<IDependencyProvider> dependencyProviders =
            ProjectAssemblies
            .SelectMany(x => x.ExportedTypes)
            .Where(x => typeof(IDependencyProvider).IsAssignableFrom(x))
            .Select(x => (IDependencyProvider)Activator.CreateInstance(x)!)
            .ToList();

        foreach(var dependencyProvider in dependencyProviders)
        {
            dependencyProvider.GetDependencies(services, ProjectConfigurations, ProjectAssemblies);
        }

        return services;
    }

    private async Task ScheduleCronJobs(WebApplication app)
    {
        ISchedulerFactory schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
        IScheduler scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJobs(QuartzJobScheduler.GetJobs(), false);
    }
}