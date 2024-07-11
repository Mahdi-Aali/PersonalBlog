using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

namespace PersonalBlog.SSO.Infrastructure.Database;

public partial class SSODbContext : IdentityDbContext<PersonalBlogUser, PersonalBlogRole, Guid>
{
    private readonly IPublisher _publisher;
    private readonly ILogger<SSODbContext> _logger;


    public SSODbContext(IPublisher publisher, ILogger<SSODbContext> logger, DbContextOptions<SSODbContext> options) : base(options)
    {
        _publisher = publisher;
        _logger = logger;
    }
}