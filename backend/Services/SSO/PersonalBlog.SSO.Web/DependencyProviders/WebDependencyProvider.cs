using Microsoft.AspNetCore.Authentication.Cookies;
using PersonalBlog.BuildingBlocks.DependencyResolver.DependencyProviderContracts;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using PersonalBlog.SSO.Infrastructure.Database;
using Quartz;
using Serilog;
using System.Reflection;

namespace PersonalBlog.SSO.Web.DependencyProviders
{
    public class WebDependencyProvider : IDependencyProvider
    {
        public IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            AddControllerWithViews(services);
            AddRazorPages(services);
            AddIdentity(services);
            ConfigureCors(services, configuration);
            ConfigureCookieAuthentication(services);
            EnableDistributionCache(services);
            AddRedisCache(services, configuration);
            EnableQuartz(services);
            AddSerilog(services);

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

        public virtual IServiceCollection ConfigureCors(IServiceCollection services, IConfiguration configuration)
        {
            string[] allowedOrigins = configuration.GetSection("AllowedCorsOrigins").Get<string[]>()!;
            services.AddCors(cfg =>
            {
                cfg.AddPolicy(configuration["CorsName"]!, corsCfg =>
                {
                    corsCfg
                    .AllowAnyHeader()
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod();
                });
            });

            return services;
        }


        public virtual IServiceCollection ConfigureCookieAuthentication(IServiceCollection services)
        {
            services.Configure<CookieAuthenticationOptions>(cfg =>
            {
                cfg.LoginPath = "/Login";
                cfg.LogoutPath = "/Logout";
                cfg.AccessDeniedPath = "/AccessDenied";
                cfg.ExpireTimeSpan = TimeSpan.FromDays(20);
                cfg.SlidingExpiration = true;
            });


            return services;
        }

        public virtual IServiceCollection AddControllerWithViews(IServiceCollection services)
        {
            services.AddControllersWithViews();

            return services;
        }


        public virtual IServiceCollection AddRazorPages(IServiceCollection services)
        {
            services.AddRazorPages();

            return services;
        }

        public virtual IServiceCollection EnableDistributionCache(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            return services;
        }

        public virtual IServiceCollection AddRedisCache(IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(cfg =>
            {
                cfg.InstanceName = "redis-cache";
                cfg.Configuration = configuration.GetConnectionString("Redis");
            });

            return services;
        }

        public virtual IServiceCollection EnableQuartz(IServiceCollection services)
        {
            services.AddQuartz();
            services.AddQuartzHostedService(cfg =>
            {
                cfg.WaitForJobsToComplete = true;
            });

            return services;
        }

        public virtual IServiceCollection AddSerilog(IServiceCollection services)
        {
            services.AddSerilog();
            return services;
        }
    }
}
