using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;

public interface IDependencyProvider
{
    public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies);

}