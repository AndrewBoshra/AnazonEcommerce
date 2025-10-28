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

namespace Anazon.Features.Auth;


public static class RegisterUser
{

    public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Phone,
        string Email,
        string Password,
        string PasswordConfirm
    ) : IRequest<Result<UserAuthInfo>>;




    public class Validator : AbstractValidator<RegisterUserCommand>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Phone)
                        .NotEmpty()
                        .MaximumLength(20)
                        .Matches(@"^\+?[1-9]\d{1,14}$")
                        .WithMessage("Phone number must be in E.164 format.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
            RuleFor(x => x.PasswordConfirm)
                        .Equal(x => x.Password)
                        .WithMessage("Passwords do not match.");
        }
    }



    public class Handler(
        AppDbContext dbContext,
        TokenService tokenService,
        UserDuplicateChecker duplicateChecker,
        RoleService roleService
        ) : IRequestHandler<RegisterUserCommand, Result<UserAuthInfo>>
    {



        public async Task<Result<UserAuthInfo>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var phone = request.Phone.Trim();
            var firstName = request.FirstName.Trim();
            var lastName = request.LastName.Trim();

            var alreadyInUseResult = await duplicateChecker.CheckAsync(email, phone, cancellationToken);

            if (!alreadyInUseResult.IsSuccess)
            {
                return Result.Failure<UserAuthInfo>(alreadyInUseResult.Error);
            }

            var defaultRole = await roleService.GetDefaultRole(cancellationToken);
            User newUser = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                RegisteredAt = DateTime.UtcNow,
                Status = UserStatus.Active,
                Roles = [
                    new(){ Role = defaultRole }
                ]
            };

            RefreshToken refreshToken = await tokenService.GenerateRefreshToken(newUser, cancellationToken);

            await dbContext.Users.AddAsync(newUser, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            string token = tokenService.GenerateJwt(newUser);


            return Result.Success(newUser.ToUserAuthInfo(token, refreshToken.Token));
        }
    }


}



public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AppRoutes.BaseAuth + "/register", async (RegisterUser.RegisterUserCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        })
        .WithTags(AppRouteTags.Auth);
    }
}