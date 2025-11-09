using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nutryon.Domain.Abstractions;

namespace Nutryon.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connStr)
    {
        services.AddDbContext<NutryonDbContext>(opt => opt.UseOracle(connStr));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRefeicaoRepository, RefeicaoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly NutryonDbContext _ctx;
    public UnitOfWork(NutryonDbContext ctx) => _ctx = ctx;
    public Task<int> SaveChangesAsync(CancellationToken ct) => _ctx.SaveChangesAsync(ct);
}