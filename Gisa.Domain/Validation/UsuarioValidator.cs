using FluentValidation;

namespace Gisa.Domain.Validation
{
    public class UsuarioValidator : BaseValidator<Usuario>
    {
        #region [ Constructor ]

        public UsuarioValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(150).WithMessage("Nome deve conter no máximo 150 caracteres");
            RuleFor(x => x.Login).NotNull().NotEmpty().WithMessage("Login não informado");
            RuleFor(x => x.Login).MaximumLength(50).WithMessage("Login deve conter no máximo 50 caracteres");
            RuleFor(x => x.Senha).NotNull().NotEmpty().WithMessage("Senha não informado");
            RuleFor(x => x.Senha).MaximumLength(150).WithMessage("Senha deve conter no máximo 150 caracteres");
            RuleFor(x => x.Perfil).NotNull().NotEmpty().WithMessage("Perfil não informado");
            RuleFor(x => x.Perfil).MaximumLength(50).WithMessage("Perfil deve conter no máximo 50 caracteres");
        }

        #endregion
    }
}
