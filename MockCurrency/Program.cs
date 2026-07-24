using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("mockcurrency"))
    .WithTracing(t => t
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter())
    .WithMetrics(m => m
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddPrometheusExporter());

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapPrometheusScrapingEndpoint();

app.MapGet("/convert/currency/USD", () =>
    Results.Json(new { currency = "PEN", value = "3.35" }));

app.MapGet("/convert/currency/PEN", () =>
    Results.Json(new { currency = "USD", value = "0.298" }));

app.Run();
