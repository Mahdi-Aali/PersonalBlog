using System.Reflection;

namespace PersonalBlog.SSO.Web.StartupConfiguration;

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

    private void LoadProjectAssemblies()
    {
        ProjectAssemblies =
            GetType()
            .Assembly
            .GetReferencedAssemblies()
            .Where(_ => _.Name!.Contains("sso", StringComparison.OrdinalIgnoreCase))
            .Select(_ => Assembly.Load(_))
            .Concat([GetType().Assembly])
            .ToArray();
    }


    private void LoadProjectConfigurations()
    {
        ProjectConfigurations = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
            .Build();
    }

}
