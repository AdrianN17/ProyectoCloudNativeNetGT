using WalletService.Api;
using WalletService.Application;
using WalletService.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // scalar/v1
}
else
{
    app.UseHsts();
}

// ✅ Esto hace que NO salga el mega detalle del DeveloperExceptionPage
app.UseExceptionHandler(/*new ExceptionHandlerOptions { SuppressDiagnosticsCallback = _ => false }*/);;
app.MapHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
