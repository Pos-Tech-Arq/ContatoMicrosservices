using Microsoft.EntityFrameworkCore;
using Prometheus;
using Regiao.AntiCorruption.BrasilApiService;
using Regiao.Api.Endpoints;
using Regiao.Infra.Configurations;
using Regiao.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRabbit();
builder.Services.ConfigureRepositories();
builder.Services.AddDomainService();
builder.Services.AddBrasilApiClientExtensions(builder.Configuration);
builder.AddFluentValidationEndpointFilter();
builder.Services.AddHealthChecks().ForwardToPrometheus();
builder.Services.ConfigureOpenTelemetry();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics();
app.RegisterRegiaoEndpoints();
app.MapMetrics();

app.Run();

public partial class Program
{
}