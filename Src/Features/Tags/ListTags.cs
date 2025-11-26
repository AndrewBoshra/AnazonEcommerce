using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Anazon.Shared.Db;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Tag;


public static class ListTags
{


    public class ListTagsQuery : BasePagedQuery, IRequest<Result<ListingResult<string>>>;



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<ListTagsQuery, Result<ListingResult<string>>>
    {
        public async Task<Result<ListingResult<string>>> Handle(ListTagsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Tags.AsNoTracking();
            if (request.Q is not null)
            {
                query = query.Where(t => t.Key.Contains(request.Q.AsNormalized()));
            }
            var pgContext = request.GetPaginationContext();

            var totalCount = await query.CountAsync(cancellationToken);
            
            query = query.ApplyPagination(pgContext);
            var tags = await query.Select(t => t.Key).ToListAsync(cancellationToken);
            var result = ListingResult<string>.FromQueryResult(pgContext, tags, totalCount);
            return Result.Success(result);

        }
    }
}


public class ListTagsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppRoutes.Tags, async ([AsParameters]ListTags.ListTagsQuery query, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Tags.List)
        .WithTags(AppRouteTags.Tags);
    }
}