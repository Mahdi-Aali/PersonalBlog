using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.BuildingBlocks.Logging;
using PersonalBlog.BuildingBlocks.Logging.Contracts;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Infrastructure.Database;
using PersonalBlog.CategoryService.Infrastructure.Repositories.CategoryAggregate;
using Quartz;
using Serilog;
using StackExchange.Redis;
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
        AddQuartz(services);
        AddSerilog(services);
        AddRedisCache(services, configuration);
        ConfigureApiBehaviorOptions(services);
        AddDataProtection(services);
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

    public virtual IServiceCollection AddQuartz(IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(cfg =>
        {
            cfg.WaitForJobsToComplete = true;
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

    public virtual IServiceCollection AddRedisCache(IServiceCollection services, IConfiguration configurations)
    {
        services.AddDistributedMemoryCache();
        services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = configurations.GetConnectionString("Redis");
            cfg.InstanceName = "RedisCache";
            cfg.ConfigurationOptions = new ConfigurationOptions()
            {
                ConnectRetry = 5
            };
        });

        return services;
    }

    public virtual IServiceCollection AddSerilog(IServiceCollection services)
    {
        services.AddSerilog();
        return services;
    }

    public virtual IServiceCollection ConfigureApiBehaviorOptions(IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(cfg =>
        {
            cfg.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    public virtual IServiceCollection AddDataProtection(IServiceCollection services)
    {
        services.AddDataProtection();
        return services;
    }
}