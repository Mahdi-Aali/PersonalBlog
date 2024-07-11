using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using PersonalBlog.SSO.Infrastructure.Database;
using System.Reflection;

namespace PersonalBlog.SSO.Web.DependencyProviders
{
    public class WebDependencyProvider : IDependencyProvider
    {
        public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            AddIdentity(services);

            return services;
        }


        public virtual IServiceCollection AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<PersonalBlogUser, PersonalBlogRole>(cfg =>
            {
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 8;
                cfg.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<SSODbContext>();


            return services;
        }
    }
}
