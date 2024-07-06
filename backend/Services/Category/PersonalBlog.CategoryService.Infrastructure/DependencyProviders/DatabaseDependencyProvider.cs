using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.CategoryService.Infrastructure.Database;
using System.Reflection;

namespace PersonalBlog.CategoryService.Infrastructure.DependencyProviders;

public class DatabaseDependencyProvider : IDependencyProvider
{
    public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        services.AddDbContext<CategoryServiceDbContext>(cfg =>
        {
            cfg.UseSqlServer(configuration.GetConnectionString("Default"), sqlServeropt =>
            {
                sqlServeropt.CommandTimeout(10);
                sqlServeropt.MigrationsAssembly("PersonalBlog.CategoryService.Api");
                sqlServeropt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
            });
            cfg.EnableDetailedErrors();
        });

        return services;
    }
}