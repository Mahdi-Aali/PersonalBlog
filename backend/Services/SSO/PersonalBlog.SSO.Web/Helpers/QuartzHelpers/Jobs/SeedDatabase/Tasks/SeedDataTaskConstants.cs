namespace PersonalBlog.SSO.Web.Helpers.QuartzHelpers.Jobs.SeedDatabase.Tasks;

public class SeedDataTaskConstants
{
    private static string[] _defaultRoles = ["Admin", "User"];
    private static string _rolePrefix = "sys";
    public static string[] DefaultRoles = _defaultRoles.Select(role => $"{_rolePrefix}_{role}").ToArray();
}
