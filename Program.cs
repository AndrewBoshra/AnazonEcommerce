using Anazon;
using Anazon.Shared.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Setup(builder.Configuration);

var app = builder.Build();

app.UseMiddlewares();

app.MapGet("/api", () => "HelloWorld")
        .RequirePermission("User_Read");
app.Run();
