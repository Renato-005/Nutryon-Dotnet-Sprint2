namespace Nutryon.Domain.Entities;

public class RefeicaoItem
{
    public long IdRefeicao { get; set; }
    public long IdIngrediente { get; set; }
    public decimal Qtde { get; set; }
    public long IdUnidade { get; set; }

    public Refeicao Refeicao { get; set; } = null!;
    public Ingrediente Ingrediente { get; set; } = null!;
    public UnidadeMedida Unidade { get; set; } = null!;
}