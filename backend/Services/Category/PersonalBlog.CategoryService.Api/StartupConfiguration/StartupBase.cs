using System.Reflection;

namespace PersonalBlog.CategoryService.Api.StartupConfiguration;

public abstract class StartupBase
{
    public IEnumerable<Assembly> ProjectAssemblies { get; private set; } = Enumerable.Empty<Assembly>();
    public IConfiguration ProjectConfigurations { get; private set; } = null!;

    protected StartupBase()
    {
        LoadProjectAssemblies();
        LoadProjectConfigurations();
    }


    public virtual void ConfigureService(WebApplicationBuilder builder)
    {

    }

    public virtual async Task Configure(WebApplication app, IWebHostEnvironment env)
    {

        await app.RunAsync();
    }





    private void LoadProjectConfigurations()
    {
        ProjectConfigurations = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
            .Build();
    }

    private void LoadProjectAssemblies()
    {
        ProjectAssemblies =
            Assembly
            .GetExecutingAssembly()
            .GetReferencedAssemblies()
            .Select(asm => Assembly.Load(asm))
            .Where(asm => asm.GetName().Name!.Contains("CategoryService", StringComparison.OrdinalIgnoreCase))
            .Concat([GetType().Assembly])
            .ToArray();
    }
}
