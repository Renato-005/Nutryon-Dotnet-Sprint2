using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Nutryon.Api.Middlewares;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
        try
        {
            await next(ctx);
        }
        catch (InvalidOperationException ex) { await Write(ctx, HttpStatusCode.Conflict, "Conflito", ex.Message); }
        catch (ArgumentException ex)       { await Write(ctx, HttpStatusCode.BadRequest, "Erro de validação", ex.Message); }
        catch (KeyNotFoundException ex)    { await Write(ctx, HttpStatusCode.NotFound, "Não encontrado", ex.Message); }
        catch (Exception ex)               { await Write(ctx, HttpStatusCode.InternalServerError, "Erro interno", ex.Message); }
    }

    static Task Write(HttpContext c, HttpStatusCode code, string title, string detail)
    {
        c.Response.ContentType = "application/problem+json";
        c.Response.StatusCode  = (int)code;
        var problem = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{(int)code}",
            Title = title,
            Status = (int)code,
            Detail = detail,
            Instance = c.Request.Path
        };
        return c.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}