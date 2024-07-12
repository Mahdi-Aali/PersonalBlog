using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using System.Reflection;

namespace PersonalBlog.SSO.Application.DependencyProviders;

public class ApplicationDependencyProvider : IDependencyProvider
{
    public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        AddMediatR(services, assemblies);

        return services;
    }


    public IServiceCollection AddMediatR(IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
        });

        return services;
    }
}
