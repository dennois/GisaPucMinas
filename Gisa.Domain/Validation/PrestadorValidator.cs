using FluentValidation;

namespace Gisa.Domain.Validation
{
    public class PrestadorValidator : BaseValidator<Prestador>
    {
        #region [ Constructor ]

        public PrestadorValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(500).WithMessage("Nome deve conter no máximo 500 caracteres");
            RuleFor(x => x.Documento).MaximumLength(50).WithMessage("Documento deve conter no máximo 50 caracteres");
        }

        #endregion
    }
}
