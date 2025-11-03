
using Anazon.Configs;
using Anazon.Shared.Authorization;
using Carter;

namespace Anazon;


public static partial class Middlewares
{

    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseOutputCache();
        app.MapCarter();
        app.UseExceptionHandler(_=> { });
        if(app.Environment.IsDevelopment())
        {
            app.MapOpenApi().CacheOutput();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(Config.SWAGGER_ENDPOINT, Config.SWAGGER_TITLE);
            });
        }
    }
    

}


