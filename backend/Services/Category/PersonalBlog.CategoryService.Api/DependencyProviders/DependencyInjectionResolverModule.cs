using Autofac;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using PersonalBlog.BuildingBlocks.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.SeedWorker;
using PersonalBlog.CategoryService.Infrastructure.Database;
using PersonalBlog.CategoryService.Infrastructure.Repositories.CategoryAggregate;
using System.Reflection;

namespace PersonalBlog.CategoryService.Api.DependencyProviders;

public class DependencyInjectionResolverModule : Autofac.Module
{
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<Assembly> _assemblies;

    public DependencyInjectionResolverModule()
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
            .Build();


        _assemblies = Assembly.GetExecutingAssembly()
            .GetReferencedAssemblies()
            .Where(asm => asm.Name!.Contains("categoryservice", StringComparison.OrdinalIgnoreCase))
            .Select(asm => Assembly.Load(asm))
            .ToArray()
            .Concat([Assembly.GetExecutingAssembly()]);
    }


    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterAssemblyTypes([.._assemblies]).AsClosedTypesOf(typeof(IRepository<>)).InstancePerLifetimeScope(); ;
        builder.RegisterAssemblyTypes([.. _assemblies]).AsClosedTypesOf(typeof(DbContextHandlerBase<,>)).InstancePerLifetimeScope();
        builder.RegisterType<DefaultLoggerWithElastic>().As<Logging.ILogger>().SingleInstance();
        builder.RegisterInstance(
        new ElasticsearchClient(
            new ElasticsearchClientSettings(new Uri(_configuration.GetConnectionString("elastic-search")!))
            .Authentication(new ApiKey(_configuration["ElasticApiKey"]!)
            ))).SingleInstance();
    }
}
