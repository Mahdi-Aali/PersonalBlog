using Autofac;
using PersonalBlog.BuildingBlocks.Logging;
using Logging = PersonalBlog.BuildingBlocks.Logging.Contracts;
using System.Reflection;

namespace PersonalBlog.SSO.Web.DependencyProviders;

public class DependencyInjectionResolverModule : Autofac.Module
{
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<Assembly> _assemblis;

    public DependencyInjectionResolverModule()
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
            .Build();


        _assemblis = Assembly.GetExecutingAssembly()
            .GetReferencedAssemblies()
            .Where(asm => asm.Name!.Contains("sso", StringComparison.OrdinalIgnoreCase))
            .Select(asm => Assembly.Load(asm))
            .ToArray()
            .Concat([Assembly.GetExecutingAssembly()]);
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);


        builder.RegisterType<DefaultLoggerWithElastic>().As<Logging::ILogger>().InstancePerLifetimeScope();

    }
}
