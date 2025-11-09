
using System.ComponentModel.DataAnnotations;

namespace Nutryon.Web.ViewModels;

public class RefeicaoVM
{
    public long? IdRefeicao { get; set; }

    [Required]
    public long IdUsuario { get; set; }

    [Required]
    [Display(Name="Tipo de Refeição")]
    public long IdTipoRefeicao { get; set; }

    [Required, DataType(DataType.Date)]
    [Display(Name="Data")]
    public DateTime DtRef { get; set; } = DateTime.Today;

    [StringLength(200)]
    public string? Observacao { get; set; }
}
