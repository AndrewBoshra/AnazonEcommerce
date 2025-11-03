using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Shared.Contracts;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class UpdateCategory
{


    public record UpdateCategoryCommand(
        UpdateCategoryData Data,
        int Id
    ) : IRequest<Result<Shared.Contracts.Category>>;

    public record UpdateCategoryData(
        string? Name,
        string? Description,
        int? ParentCategoryId
    );






    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<UpdateCategoryCommand, Result<Shared.Contracts.Category>>
    {


        public async Task<Result<Shared.Contracts.Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var Category = await dbContext.Categories.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            var data = request.Data;
           
            var validParent = data.ParentCategoryId is null || await dbContext.Categories.AnyAsync(c => c.Id == data.ParentCategoryId, cancellationToken: cancellationToken);
            if (!validParent) return Result.Failure<Shared.Contracts.Category>(Error.CategoryInvalidParentId);

            if (Category == null)
            {
                return Result.Failure<Shared.Contracts.Category>(Error.CategoryNotFound);
            }
            Category.Name = data.Name ?? Category.Name;
            Category.Description = data.Description ?? Category.Description;

            dbContext.Categories.Update(Category);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(Category.ToCategoryContract());
        }
    }


}
public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch(AppRoutes.Categories + "/{id:int}", async (int id, UpdateCategory.UpdateCategoryData data, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCategory.UpdateCategoryCommand(
                Data: data,
                Id: id
            );
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                () => result.ToSuccessHttpResult(),
                error => error == Error.CategoryNotFound ? result.ToNotFoundHttpResult() : result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.Category.Update)
        .WithTags(AppRouteTags.Categories);
    }
}