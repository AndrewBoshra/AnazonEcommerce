using Anazon.Configs;
using Anazon.Database;
using Anazon.Models;
using Anazon.Shared;
using Anazon.Shared.Contracts;
using Anazon.Shared.Services;
using Anazon.Utils;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anazon.Features.Auth;


public static class Login
{

    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<Result<UserAuthInfo>>;




    public class Validator : AbstractValidator<LoginCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty();
        }
    }



    public class Handler(
        AppDbContext dbContext,
        TokenService tokenService
        ) : IRequestHandler<LoginCommand, Result<UserAuthInfo>>
    {



        public async Task<Result<UserAuthInfo>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await dbContext.Users
                        .Include(u=>u.Roles)
                        .ThenInclude(r=>r.Role)
                        .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            
            if (user == null || !PasswordHasher.VerifyHash(request.Password, user.PasswordHash)) 
                return Result.Failure<UserAuthInfo>(Error.InvalidCredentials);


            (string token, RefreshToken refreshToken) =  await tokenService.GenerateTokens(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(user.ToUserAuthInfo(token, refreshToken.Token));
        }
    }


}



public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.BaseAuth + "/login", async (Login.LoginCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Match(
                onSuccess: () => result.ToSuccessHttpResult(),
                onFailure: (_) => result.ToUnauthorized()
            );
        })
        .WithTags(AppRouteTags.Auth);
    }
}