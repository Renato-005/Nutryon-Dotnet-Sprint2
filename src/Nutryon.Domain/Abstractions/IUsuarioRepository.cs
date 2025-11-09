using Nutryon.Domain.Entities;

namespace Nutryon.Domain.Abstractions;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(long id, CancellationToken ct);
    Task<List<Usuario>> ListAsync(CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task AddAsync(Usuario usuario, CancellationToken ct);
    Task RemoveAsync(Usuario usuario, CancellationToken ct);
}