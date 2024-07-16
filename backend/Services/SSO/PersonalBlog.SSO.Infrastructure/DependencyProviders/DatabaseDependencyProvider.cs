using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.SSO.Infrastructure.Database;
using System.Reflection;

namespace PersonalBlog.SSO.Infrastructure.DependencyProviders;

public class DatabaseDependencyProvider : IDependencyProvider
{
    public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        RegisterDbContext(services, configuration);
        return services;
    }

    public virtual IServiceCollection RegisterDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SSODbContext>(cfg =>
        {
            cfg.UseSqlServer(configuration.GetConnectionString("default"), sqlcfg =>
            {
                sqlcfg
                .EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null)
                .MigrationsAssembly("PersonalBlog.SSO.Web");
            })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
        });

        return services;
    }

}
