
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;

namespace PersonalBlog.CategoryService.Api.StartupConfiguration;

public abstract class ApiStartup : StartupBase
{
    public override void ConfigureService(WebApplicationBuilder builder)
    {
        base.ConfigureService(builder);

        IServiceCollection services = builder.Services;
        ResolveAssembliesDependencies(services);
    }

    public override async Task Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
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
}
