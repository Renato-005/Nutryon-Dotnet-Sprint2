namespace Nutryon.Application.Refeicoes;

public record RefeicaoItemCreateDto(long IdIngrediente, decimal Qtde, long IdUnidade);
public record RefeicaoCreateDto(long IdUsuario, long IdTipoRefeicao, DateOnly DtRef, string? Observacao, IEnumerable<RefeicaoItemCreateDto> Itens);
public record RefeicaoDiaItemDto(long IdIngrediente, decimal Qtde, long IdUnidade);
public record RefeicaoDiaDto(long IdRefeicao, long IdTipoRefeicao, DateOnly DtRef, string? Observacao, IEnumerable<RefeicaoDiaItemDto> Itens);