using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate.DomainEvents;

public class NewRoleCreatedDomainEvents : INotification
{

    public required Guid RoleId { get; set; }
    public required string RoleName { get; set; } = string.Empty;

    [SetsRequiredMembers]
    public NewRoleCreatedDomainEvents(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}
