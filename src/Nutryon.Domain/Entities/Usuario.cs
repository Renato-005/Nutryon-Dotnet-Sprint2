namespace Nutryon.Domain.Entities;

public class Usuario
{
    public long IdUsuario { get; set; }
    public string NomeUsuario { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly? DtNasc { get; set; }
    public DateOnly DtCriacao { get; set; }
    public DateOnly? DtAtualizacao { get; set; }

    public ICollection<Refeicao> Refeicoes { get; set; } = new List<Refeicao>();
}