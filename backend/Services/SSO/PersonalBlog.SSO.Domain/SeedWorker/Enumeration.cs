namespace PersonalBlog.SSO.Domain.SeedWorker;

public abstract class Enumeration
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;

    protected Enumeration(int id, string title) => (Id, Title) = (id, title);
}
