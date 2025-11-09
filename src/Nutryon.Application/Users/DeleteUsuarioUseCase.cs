using Nutryon.Domain.Abstractions;

namespace Nutryon.Application.Users;

public class DeleteUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IUnitOfWork _uow;
    public DeleteUsuarioUseCase(IUsuarioRepository repo, IUnitOfWork uow) => (_repo,_uow) = (repo,uow);

    public async Task Handle(long id, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Usuário não encontrado.");
        await _repo.RemoveAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }
}