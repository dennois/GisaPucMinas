using FluentValidation;

namespace Gisa.Domain.Validation
{
    public class EspecialidadeValidator : BaseValidator<Especialidade>
    {
        #region [ Constructor ]

        public EspecialidadeValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(50).WithMessage("Nome deve conter no máximo 50 caracteres");
            RuleFor(x => x.Codigo).MaximumLength(50).WithMessage("Código deve conter no máximo 50 caracteres");
        }

        #endregion
    }
}
