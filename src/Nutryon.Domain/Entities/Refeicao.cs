namespace Nutryon.Domain.Entities;

public class Refeicao
{
    public long IdRefeicao { get; set; }
    public long IdUsuario { get; set; }
    public long IdTipoRefeicao { get; set; }
    public DateOnly DtRef { get; set; }
    public string Situacao { get; set; } = "ATIVA";
    public string? Observacao { get; set; }
    public DateOnly DtCriacao { get; set; }
    public DateOnly? DtAtualizacao { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public TipoRefeicao Tipo { get; set; } = null!;
    public ICollection<RefeicaoItem> Itens { get; set; } = new List<RefeicaoItem>();
}