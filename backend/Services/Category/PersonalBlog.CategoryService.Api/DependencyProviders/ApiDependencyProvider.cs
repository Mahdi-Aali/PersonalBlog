using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using System.Reflection;
using System.Text.Json.Serialization;

namespace PersonalBlog.CategoryService.Api.DependencyProviders;

public class ApiDependencyProvider : IDependencyProvider
{
    public virtual IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        AddEndpointExplorer(services);
        AddSwaggerGen(services);
        AddControllers(services);
        AddCors(services, configuration);
        return services;
    }   

    public virtual IServiceCollection AddEndpointExplorer(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        return services;
    }

    public virtual IServiceCollection AddSwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }

    public virtual IServiceCollection AddControllers(IServiceCollection services)
    {
        services.AddControllers(cfg =>
        {
            cfg.RespectBrowserAcceptHeader = true;
            cfg.ReturnHttpNotAcceptable = true;
            cfg.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        })
            .AddJsonOptions(cfg =>
            {
                cfg.JsonSerializerOptions.WriteIndented = true;
                cfg.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        return services;
    }

    public virtual IServiceCollection AddCors(IServiceCollection services, IConfiguration configuration)
    {
        string[] allowedOrigins = configuration.GetSection("CorsAllowedOrigins").Get<string[]>()!;
        if (allowedOrigins.Length < 1)
        {
            throw new NullReferenceException("No cors origin loaded.");
        }
        if (string.IsNullOrEmpty(configuration["CorsName"]))
        {
            throw new NullReferenceException("Failed to fetch cors name from application settings.");
        }

        services.AddCors(cfg =>
        {
            cfg.AddPolicy(configuration["CorsName"]!, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        return services;
    }
}