using Nutryon.Domain.Abstractions;
using Nutryon.Domain.Entities;

namespace Nutryon.Application.Refeicoes;

public class CreateRefeicaoComItensUseCase
{
    private readonly IRefeicaoRepository _refeicoes;
    private readonly IUnitOfWork _uow;

    public CreateRefeicaoComItensUseCase(IRefeicaoRepository refeicoes, IUnitOfWork uow)
        => (_refeicoes, _uow) = (refeicoes, uow);

    public async Task<long> Handle(RefeicaoCreateDto dto, CancellationToken ct)
    {
        var entity = new Refeicao
        {
            IdUsuario = dto.IdUsuario,
            IdTipoRefeicao = dto.IdTipoRefeicao,
            DtRef = dto.DtRef,
            Observacao = dto.Observacao,
            DtCriacao = DateOnly.FromDateTime(DateTime.UtcNow),
            Itens = dto.Itens.Select(i => new RefeicaoItem
            {
                IdIngrediente = i.IdIngrediente,
                Qtde = i.Qtde,
                IdUnidade = i.IdUnidade
            }).ToList()
        };

        await _refeicoes.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return entity.IdRefeicao;
    }
}