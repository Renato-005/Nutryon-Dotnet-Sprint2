namespace Nutryon.Domain.Entities;

public class Ingrediente
{
    public long IdIngrediente { get; set; }
    public string NomeIngrediente { get; set; } = null!;
    public long? IdCategoria { get; set; }
    public DateOnly DtCriacao { get; set; }
    public DateOnly? DtAtualizacao { get; set; }

    public ICollection<RefeicaoItem> Itens { get; set; } = new List<RefeicaoItem>();
}