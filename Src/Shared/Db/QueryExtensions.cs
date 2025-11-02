using Anazon.Shared.Contracts;

namespace Anazon.Shared.Db;

public static class QueryExtensions
{

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationContext context)
    {
        return query
            .Skip((context.Page - 1) * context.PageSize)
            .Take(context.PageSize);
    }

}

