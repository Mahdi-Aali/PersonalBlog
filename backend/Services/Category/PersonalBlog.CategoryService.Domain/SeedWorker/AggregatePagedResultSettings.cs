namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public class AggregatePagedResultSettings
{
    public int ItemPerPage { get; set; }
    public int PageId { get; set; }

    protected AggregatePagedResultSettings()
    {

    }

    public static AggregatePagedResultSettings Default => new() { ItemPerPage = 25, PageId = 0 };
    public static AggregatePagedResultSettingsFactory Factory => new AggregatePagedResultSettingsFactory();


    public class AggregatePagedResultSettingsFactory
    {
        public AggregatePagedResultSettings Create(int itemPerPage, int pageId)
        {
            return new AggregatePagedResultSettings()
            {
                ItemPerPage = itemPerPage,
                PageId = pageId
            };
        }
    }
}