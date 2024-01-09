namespace Product.Application.Product.Queries.GetAllPaginated;

public class PagedResponse<TResponse>
    where TResponse : class
{
    public PagedResponse(List<TResponse> items, int page, int itemsPerPage, int pageCount)
    {
        Items = items;
        Page = page;
        ItemsPerPage = itemsPerPage;
        PageCount = pageCount;
    }

    public List<TResponse> Items { get; }
    public int Page { get; }
    public int ItemsPerPage { get; }
    public int PageCount { get; }
}
