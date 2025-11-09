using Microsoft.EntityFrameworkCore;
using Nutryon.Domain.Abstractions;
using Nutryon.Domain.Entities;

namespace Nutryon.Infrastructure.Persistence;

public class RefeicaoRepository : IRefeicaoRepository
{
    private readonly NutryonDbContext _ctx;
    public RefeicaoRepository(NutryonDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(Refeicao refeicao, CancellationToken ct)
        => await _ctx.Refeicoes.AddAsync(refeicao, ct);

    public Task<List<Refeicao>> ListByUsuarioAndDateAsync(long idUsuario, DateOnly data, CancellationToken ct)
        => _ctx.Refeicoes
                .AsNoTracking()
                .Include(r => r.Itens)
                .Where(r => r.IdUsuario == idUsuario && r.DtRef == data)
                .ToListAsync(ct);
}