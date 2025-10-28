using Carter;
using MediatR;
using System.Text;
using Anazon.Configs;
using Anazon.Database;
using FluentValidation;
using Anazon.Behaviors;
using Anazon.Shared.Services;
using Anazon.Shared.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Anazon;


public static partial class IoC
{

    public static void SetupDb(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<AuditInterceptor>();
        services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var auditInterceptor = sp.GetRequiredService<AuditInterceptor>();
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
                options.AddInterceptors(auditInterceptor);
            }
        );

    }


    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<UserDuplicateChecker>();
        services.AddTransient<RoleService>();
    }
    public static void SetupAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new JWTConfig();
        configuration.GetSection("JWT").Bind(config);

        services.AddSingleton(config);
        services.AddTransient<TokenService>();
        services.AddSingleton<Utils.JWT>();
        services.AddTransient<CurrentUserService>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
            opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey))
                };
            }
            );
        services.AddAuthorization();
    }

    public static void Setup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.SetupDb(configuration);
        services.SetupAuth(configuration);
        services.SetupServices(configuration);


        var assembly = typeof(Program).Assembly;
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddExceptionHandler<ExceptionHandlers.ValidationErrorExceptionHandler>();
        services.AddCarter();
        services.AddValidatorsFromAssembly(assembly);
        services.AddOutputCache();
        services.AddOpenApi();


    }


}


