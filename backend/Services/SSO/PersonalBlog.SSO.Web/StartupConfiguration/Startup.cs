namespace PersonalBlog.SSO.Web.StartupConfiguration;

public abstract class Startup<TStartup> where TStartup : StartupBase
{
    protected static async Task RunAsync(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        TStartup startup = (TStartup)Activator.CreateInstance(typeof(TStartup))!;

        startup.ConfigureService(builder);

        await startup.Configure(builder.Build(), builder.Environment);
    }
}
