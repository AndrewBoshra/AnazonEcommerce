namespace Anazon.Shared.Contracts;


public record class     ListingResult<T>
{
    public required IEnumerable<T> Items { get; set; }
    public required int TotalCount { get; set; }
    public required int Page { get; set; }
    public required int PageSize { get; set; }
    public required int Pages { get; set; }




    public static ListingResult<TDestination> FromQueryResult<TDestination>(
    PaginationContext paginationContext, IEnumerable<TDestination> mappedItems, int totalCount)
    {
        return new ListingResult<TDestination>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            Page = paginationContext.Page,
            PageSize = paginationContext.PageSize,
            Pages = (int)Math.Ceiling((double)totalCount / paginationContext.PageSize) 
        };
    }
}


