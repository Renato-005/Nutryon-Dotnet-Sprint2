using AutoMapper;
using Nutryon.Domain.Abstractions;
using Nutryon.Domain.Entities;

namespace Nutryon.Application.Users;

public class CreateUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateUsuarioUseCase(IUsuarioRepository repo, IUnitOfWork uow, IMapper mapper)
        => (_repo,_uow,_mapper) = (repo,uow,mapper);

    public async Task<long> Handle(UsuarioCreateDto dto, CancellationToken ct)
    {
        if (await _repo.EmailExistsAsync(dto.Email, ct))
            throw new InvalidOperationException("E-mail já cadastrado.");

        var entity = _mapper.Map<Usuario>(dto);
        entity.DtCriacao = DateOnly.FromDateTime(DateTime.UtcNow);

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return entity.IdUsuario;
    }
}