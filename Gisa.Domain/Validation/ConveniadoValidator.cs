using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Validation
{
    public class ConveniadoValidator : BaseValidator<Conveniado>
    {
        #region [ Constructor ]

        public ConveniadoValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome não informado");
            RuleFor(x => x.Nome).MaximumLength(150).WithMessage("Nome deve conter no máximo 150 caracteres");
            RuleFor(x => x.Codigo).MaximumLength(50).WithMessage("Código deve conter no máximo 10 caracteres");
            RuleFor(x => x.Endereco).Must(ValidarNull).WithMessage("Endereço não informado");
        }

        #endregion

        private bool ValidarNull(BaseDomain model)
        {
            return model != null;
        }
    }
}
