using PersonalBlog.CategoryService.Api.StartupConfiguration;
using PersonalBlog.CategoryService.Application;
using PersonalBlog.CategoryService.Domain;
using PersonalBlog.CategoryService.Infrastructure;

public class Program : Startup<CategoryServiceStartup>
{
    static async Task Main(string[] args)
    {
        await RunAsync(args);
    }
}


public class CategoryServiceStartup : ApiStartup
{

}

public class ReferencedAssemblies
{
    private CategoryServiceApplicationAssemblyReference _applicationReference = null!;
    private CategoryServiceDomainApplicationReference _domainReference = null!;
    private CategoryServiceInfrastructureAssemblyReference _infrastructureReference = null!;
}