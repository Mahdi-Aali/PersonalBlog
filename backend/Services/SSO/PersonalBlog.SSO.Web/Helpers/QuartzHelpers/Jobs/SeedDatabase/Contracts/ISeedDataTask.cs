namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Contracts;

public interface ISeedDataTask
{
    public byte Pritory { get; set; }
    public Task ExecuteAsync(IServiceProvider serviceProvider);
}
