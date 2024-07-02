using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using System.Reflection;

namespace PersonalBlog.CategoryService.Infrastructure.DependencyProviders;

public class DatabaseDependencyProvider : IDependencyProvider
{
    public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        return services;
    }
}
