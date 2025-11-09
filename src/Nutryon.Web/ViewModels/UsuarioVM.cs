
using System.ComponentModel.DataAnnotations;

namespace Nutryon.Web.ViewModels;

public class UsuarioVM
{
    public long? IdUsuario { get; set; }

    [Required, StringLength(120)]
    [Display(Name="Nome")]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name="Data de Nascimento")]
    [DataType(DataType.Date)]
    public DateTime? DtNasc { get; set; }
}
