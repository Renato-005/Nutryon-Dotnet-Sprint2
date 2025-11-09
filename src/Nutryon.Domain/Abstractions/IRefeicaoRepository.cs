using Nutryon.Domain.Entities;

namespace Nutryon.Domain.Abstractions;

public interface IRefeicaoRepository
{
    Task AddAsync(Refeicao refeicao, CancellationToken ct);
    Task<List<Refeicao>> ListByUsuarioAndDateAsync(long idUsuario, DateOnly data, CancellationToken ct);
}