using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Authorization;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class AddAttributeValuesBulk
{

    public record AttributeValues(
        List<string> Values
    );

    public record AddAttributeValuesBulkCommand(
        AttributeValues Values,
        int AttributeId
    ) : IRequest<Result>;




    public class Validator : AbstractValidator<AddAttributeValuesBulkCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Values)
                .ChildRules(values => values.RuleFor(vs => vs.Values).NotEmpty())
                .OverridePropertyName(""); // to hide the 'Values.' prefix in errors
            RuleFor(x => x.AttributeId).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext
        ) : IRequestHandler<AddAttributeValuesBulkCommand, Result>
    {
        public async Task<Result> Handle(AddAttributeValuesBulkCommand request, CancellationToken cancellationToken)
        {

            var validAttribute = await dbContext.Attributes
                .AnyAsync(c => c.Id == request.AttributeId, cancellationToken);
            if (!validAttribute) return Result.Failure<Shared.Contracts.Attribute>(Error.CategoryInvalidId);

            var existingValues = await dbContext.AttributeValues
                .Where(av => av.AttributeId == request.AttributeId)
                .Select(av => av.Value)
                .ToListAsync(cancellationToken);

            var newValues = request.Values.Values
                .Select(v => v.AsNormalized())
                .ToList();

            var alreadyExistingValues = newValues
                .Where(existingValues.Contains)
                .ToList();

            if (alreadyExistingValues.Count != 0)
            {
                return Result.Failure<Shared.Contracts.Attribute>(Error.CategoryAttributeValuesAlreadyExist(alreadyExistingValues));
            }
            dbContext.AttributeValues.AddRange(
                newValues.Select(v => new AttributeValue
                {
                    Value = v,
                    AttributeId = request.AttributeId
                })
            );

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }


}
public class AddAttributeValuesBulkEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.AttributeValues, async (int AttributeId, AddAttributeValuesBulk.AttributeValues values, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new AddAttributeValuesBulk.AddAttributeValuesBulkCommand(values, AttributeId), cancellationToken);
            return result.Match(
                () => result.ToCreatedHttpResult(),
                error => result.ToBadRequestHttpResult()
            );
        })
        .RequirePermission(Permissions.AttributeValue.Create)
        .WithTags([
            AppRouteTags.Attributes,
            AppRouteTags.AttributeValues
        ]);
    }
}