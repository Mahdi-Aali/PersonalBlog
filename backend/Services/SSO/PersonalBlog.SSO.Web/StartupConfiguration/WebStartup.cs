using Autofac;
using Autofac.Extensions.DependencyInjection;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.SSO.Web.Helpers.QuartzHelpers;
using Quartz;
using Serilog;

namespace PersonalBlog.SSO.Web.StartupConfiguration;

public abstract class WebStartup : StartupBase
{
    public override void ConfigureService(WebApplicationBuilder builder)
    {
        base.ConfigureService(builder);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
            .CreateLogger();


        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(cfg =>
            {
                cfg.RegisterAssemblyModules(ProjectAssemblies.ToArray());
            });

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

        app.UseRouting();

        app.UseCors(ProjectConfigurations["CorsName"]!);

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapDefaultControllerRoute();
        app.MapGet("/hc", async (HttpContext context) =>
        {
            await context.Response.WriteAsync("Healthy...");
        });

        await ScheduleCronJobs(app);

        await base.Configure(app, env);
    }

    private IServiceCollection ResolveAssembliesDependencies(IServiceCollection services)
    {
        List<IDependencyProvider> dependencyProviders = [];
        ProjectAssemblies
            .SelectMany(_ => _.ExportedTypes.Where(_ => typeof(IDependencyProvider).IsAssignableFrom(_)))
            .ToList()
            .ForEach(_ =>
            {
                dependencyProviders.Add((IDependencyProvider)Activator.CreateInstance(_)!);
            });

        foreach (IDependencyProvider dependencyProvider in dependencyProviders)
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
