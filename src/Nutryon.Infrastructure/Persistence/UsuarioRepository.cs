using Microsoft.EntityFrameworkCore;
using Nutryon.Domain.Abstractions;
using Nutryon.Domain.Entities;

namespace Nutryon.Infrastructure.Persistence;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly NutryonDbContext _ctx;
    public UsuarioRepository(NutryonDbContext ctx) => _ctx = ctx;

    public Task<Usuario?> GetByIdAsync(long id, CancellationToken ct)
        => _ctx.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id, ct);

    public Task<List<Usuario>> ListAsync(CancellationToken ct)
        => _ctx.Usuarios.AsNoTracking().ToListAsync(ct);

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        => _ctx.Usuarios.AnyAsync(x => x.Email == email, ct);

    public async Task AddAsync(Usuario usuario, CancellationToken ct)
        => await _ctx.Usuarios.AddAsync(usuario, ct);

    public Task RemoveAsync(Usuario usuario, CancellationToken ct)
    {
        _ctx.Usuarios.Remove(usuario);
        return Task.CompletedTask;
    }
}