namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public abstract class Enumeration
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;


    protected Enumeration(int id, string title)
    {
        Id = id;
        Title = title;
    }
}