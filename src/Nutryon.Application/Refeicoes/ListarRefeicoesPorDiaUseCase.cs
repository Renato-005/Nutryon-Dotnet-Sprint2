using Nutryon.Domain.Abstractions;

namespace Nutryon.Application.Refeicoes;

public class ListarRefeicoesPorDiaUseCase
{
    private readonly IRefeicaoRepository _repo;
    public ListarRefeicoesPorDiaUseCase(IRefeicaoRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<RefeicaoDiaDto>> Handle(long idUsuario, DateOnly data, CancellationToken ct)
    {
        var list = await _repo.ListByUsuarioAndDateAsync(idUsuario, data, ct);
        return list.Select(r => new RefeicaoDiaDto(
            r.IdRefeicao,
            r.IdTipoRefeicao,
            r.DtRef,
            r.Observacao,
            r.Itens.Select(i => new RefeicaoDiaItemDto(i.IdIngrediente, i.Qtde, i.IdUnidade))
        )).ToList();
    }
}