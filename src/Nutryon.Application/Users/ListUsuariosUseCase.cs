using AutoMapper;
using Nutryon.Domain.Abstractions;

namespace Nutryon.Application.Users;

public class ListUsuariosUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IMapper _mapper;
    public ListUsuariosUseCase(IUsuarioRepository repo, IMapper mapper) => (_repo,_mapper) = (repo,mapper);

    public async Task<IReadOnlyList<UsuarioReadDto>> Handle(CancellationToken ct)
    {
        var list = await _repo.ListAsync(ct);
        return list.Select(u => new UsuarioReadDto(u.IdUsuario, u.NomeUsuario, u.Email)).ToList();
    }
}