
namespace Anazon.Shared.Contracts;


public record PaginationContext
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public static class PaginationExtensions
{
    public static PaginationContext GetPaginationContext(this BasePagedQuery query)
    {
        return new PaginationContext
        {
            Page = query.Page ?? 1,
            PageSize = query.PageSize ?? 10
        };
    }
}
