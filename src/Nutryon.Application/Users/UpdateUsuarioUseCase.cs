using AutoMapper;
using Nutryon.Domain.Abstractions;

namespace Nutryon.Application.Users;

public class UpdateUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UpdateUsuarioUseCase(IUsuarioRepository repo, IUnitOfWork uow, IMapper mapper)
        => (_repo,_uow,_mapper) = (repo,uow,mapper);

    public async Task Handle(long id, UsuarioUpdateDto dto, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Usuário não encontrado.");
        entity.NomeUsuario = dto.NomeUsuario;
        entity.Email = dto.Email;
        entity.DtAtualizacao = DateOnly.FromDateTime(DateTime.UtcNow);
        await _uow.SaveChangesAsync(ct);
    }
}