namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker;

public class AggregatePagedResult<TResult>
{
    public AggregatePagedResult(int pageId, int itemPerPage, int totalItems, TResult result)
    {
        PageId = pageId;
        ItemPerPage = itemPerPage;
        TotalItems = totalItems;
        Result = result;
    }

    public int PageId { get; set; }
    public int ItemPerPage { get; set; }
    public int TotalItems { get; set; }

    public TResult Result { get; set; } = default(TResult)!;
}