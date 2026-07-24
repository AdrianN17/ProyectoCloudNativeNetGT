var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/convert/currency/USD", () =>
    Results.Json(new { currency = "PEN", value = "3.35" }));

app.MapGet("/convert/currency/PEN", () =>
    Results.Json(new { currency = "USD", value = "0.298" }));

app.Run();
