using PersonalBlog.SSO.Application;
using PersonalBlog.SSO.Domain;
using PersonalBlog.SSO.Infrastructure;
using PersonalBlog.SSO.Web.StartupConfiguration;

public class Program : Startup<SSOStartup>
{
    static async Task Main(string[] args)
    {
        await RunAsync(args);
    }
}

public class SSOStartup : WebStartup
{
    
}


public class ReferencedAssemblies
{
    private SSOServiceApplicationAssemblyReference _applicationReference;
    private SSOServiceDomainApplicationReference _domainReference;
    private SSOServiceInfrastructureAssemblyReference _infrastructure;
}
