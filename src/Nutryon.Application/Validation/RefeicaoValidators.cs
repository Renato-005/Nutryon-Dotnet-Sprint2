using FluentValidation;
using Nutryon.Application.Refeicoes;

namespace Nutryon.Application.Validation;

public class RefeicaoItemCreateDtoValidator : AbstractValidator<RefeicaoItemCreateDto>
{
    public RefeicaoItemCreateDtoValidator()
    {
        RuleFor(x => x.IdIngrediente).GreaterThan(0);
        RuleFor(x => x.Qtde).GreaterThan(0);
        RuleFor(x => x.IdUnidade).GreaterThan(0);
    }
}

public class RefeicaoCreateDtoValidator : AbstractValidator<RefeicaoCreateDto>
{
    public RefeicaoCreateDtoValidator()
    {
        RuleFor(x => x.IdUsuario).GreaterThan(0);
        RuleFor(x => x.IdTipoRefeicao).GreaterThan(0);
        RuleFor(x => x.Itens).NotEmpty();
        RuleForEach(x => x.Itens).SetValidator(new RefeicaoItemCreateDtoValidator());
    }
}