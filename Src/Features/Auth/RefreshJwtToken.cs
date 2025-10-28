using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Services;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class RefreshJwtToken
{

    public record RefreshJwtTokenCommand(
        string RefreshToken
    ) : IRequest<Result<RefreshJwtTokenResponse>>;

    public record RefreshJwtTokenResponse(
        string RefreshToken,
        string JwtToken
    );
    public class Validator : AbstractValidator<RefreshJwtTokenCommand>
    {
        public Validator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext,
        TokenService tokenService
        ) : IRequestHandler<RefreshJwtTokenCommand, Result<RefreshJwtTokenResponse>>
    {



        public async Task<Result<RefreshJwtTokenResponse>> Handle(RefreshJwtTokenCommand request, CancellationToken cancellationToken)
        {

            var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken: cancellationToken);

            if (refreshToken is null || refreshToken.IsExpired)
            {
                return Result.Failure<RefreshJwtTokenResponse>(Error.ExpiredOrInvalidRefreshToken);
            }

            var user = await dbContext.Users
                        .Include(u => u.Roles)
                        .ThenInclude(r => r.Role)
                        .FirstOrDefaultAsync(u => u.Id == refreshToken.UserId, cancellationToken);

            if (user is null || !user.CanLogin)
            {
                return Result.Failure<RefreshJwtTokenResponse>(Error.UserIsDisabledOrDeleted);
            }

            (string token, RefreshToken newRefreshToken) = await tokenService.GenerateTokens(user);
            dbContext.RefreshTokens.Remove(refreshToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(new RefreshJwtTokenResponse(
                newRefreshToken.Token,
                token
            ));
        }
    }


}



public class RefreshJwtTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.BaseAuth + "/refresh", async (RefreshJwtToken.RefreshJwtTokenCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                onSuccess: () => result.ToSuccessHttpResult(),
                onFailure: (_) => result.ToBadRequestHttpResult()
            );
        })
        .WithTags(AppRouteTags.Auth);
    }
}