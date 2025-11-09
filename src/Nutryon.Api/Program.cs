
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Nutryon.Api.Middlewares;
using Nutryon.Application;
using Nutryon.Application.Users;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Nutryon.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Middleware de erro (IMiddleware)
builder.Services.AddTransient<Nutryon.Api.Middlewares.ErrorHandlerMiddleware>();

// Infra + DbContext (Oracle FIAP)
var conn = builder.Configuration.GetConnectionString("NutryonDb");
builder.Services.AddInfrastructure(conn);

// App services (UseCases, AutoMapper)
builder.Services.AddApplicationServices();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDto>();

// HealthChecks p/ Oracle
builder.Services.AddHealthChecks().AddOracle(conn);

var app = builder.Build();

// Garantir base criada (sem migrations no Oracle)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NutryonDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

// Health endpoints
app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => Results.Ok(new { api = "Nutryon", status = "ok" }));

app.Run();
