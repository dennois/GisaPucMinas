using FluentValidation;

namespace Gisa.Domain.Validation
{
    public class FluxoValidator : BaseValidator<Fluxo>
    {
        #region [ Constructor ]

        public FluxoValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(50).WithMessage("Nome deve conter no máximo 50 caracteres");
            RuleFor(x => x.Codigo).NotNull().NotEmpty().WithMessage("Código não informado");
            RuleFor(x => x.Codigo).MaximumLength(10).WithMessage("Código deve conter no máximo 10 caracteres");
            RuleFor(x => x.Processo).NotNull().NotEmpty().WithMessage("Processo não informado");
        }

        #endregion
    }
}
