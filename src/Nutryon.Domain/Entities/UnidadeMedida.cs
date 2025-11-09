namespace Nutryon.Domain.Entities;

public class UnidadeMedida
{
    public long IdUnidade { get; set; }
    public string Sigla { get; set; } = null!;
    public string? Descr { get; set; }
}