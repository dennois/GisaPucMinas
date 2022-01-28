using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Validation
{
    public class AssociadoValidator : BaseValidator<Associado>
    {
        #region [ Constructor ]

        public AssociadoValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.CPF).NotNull().NotEmpty().WithMessage("CPF não informado");
            RuleFor(x => x.CPF).MaximumLength(11).WithMessage("CPF deve conter no máximo 11 caracteres");
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(150).WithMessage("Nome deve conter no máximo 150 caracteres");
            RuleFor(x => x.RG).NotNull().NotEmpty().WithMessage("RG não informado");
            RuleFor(x => x.RG).MaximumLength(12).WithMessage("RG deve conter no máximo 12 caracteres");
            RuleFor(x => x.DataNascimento).NotNull().WithMessage("Data de nascimento não informado");
            RuleFor(x => x.Endereco).Must(ValidarNull).WithMessage("Endereço não informado");
        }

        #endregion

        private bool ValidarNull(BaseDomain model)
        {
            return model != null;
        }
    }
}
