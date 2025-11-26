using Anazon.Configs;
using Anazon.Database;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Tag;


public static class CreateTags
{



    public record CreateTagsCommand(
        List<string> Tags
    ) : IRequest<Result>;


    public class Validator : AbstractValidator<CreateTagsCommand>
    {

        public Validator()
        {
            RuleForEach(x => x.Tags).NotNull().WithMessage("Tag {CollectionIndex} is required.");
            RuleFor(x => x.Tags).NotEmpty();// MinimumLength(1).WithMessage("Tags must at least have 1 element");
        }
    }

    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<CreateTagsCommand, Result>
    {


        public async Task<Result> Handle(CreateTagsCommand request, CancellationToken cancellationToken)
        {
            var existingTags = await dbContext.Tags
                .Where(t => request.Tags.Select(StringUtils.AsNormalized).Contains(t.Key))
                .Select(t => t.Key)
                .ToListAsync(cancellationToken);

            if (existingTags.Count > 0)
            {
                return Result.Failure(Error.TagsAlreadyExists(existingTags));
            }
            var newTags = request.Tags
                .Select(t => new Models.Tag { Key = t.AsNormalized() })
                .ToList();

            dbContext.Tags.AddRange(newTags);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}


public class CreateTagsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.Tags, async (CreateTags.CreateTagsCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Tags.Create)
        .WithTags(AppRouteTags.Tags);
    }
}