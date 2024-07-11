using PersonalBlog.SSO.Domain.SeedWorker;

namespace PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

public class UserAccountStatus : Enumeration
{
    public static UserAccountStatus NotActive => new(0, nameof(NotActive));
    public static UserAccountStatus Active => new(1, nameof(Active));
    public static UserAccountStatus Diabled => new(2, nameof(Diabled));
    public static UserAccountStatus Banned => new(3, nameof(Banned));
    public static UserAccountStatus Suspend => new(4, nameof(Suspend));
    public static UserAccountStatus EmailSuspend => new(5, nameof(EmailSuspend));
    public static UserAccountStatus PhoneNumberSuspend => new(6, nameof(PhoneNumberSuspend));

    public UserAccountStatus(int id, string title) : base(id, title)
    {
    }
}
