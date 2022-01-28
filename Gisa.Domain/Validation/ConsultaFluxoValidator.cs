using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Validation
{
    public class ConsultaFluxoValidator : BaseValidator<ConsultaFluxo>
    {
        #region [ Constructor ]

        public ConsultaFluxoValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Passo).NotNull().NotEmpty().WithMessage("CPF não informado");
            RuleFor(x => x.Status).MaximumLength(1).WithMessage("Status deve conter 1 caracter");
            RuleFor(x => x.Passo).MaximumLength(10).WithMessage("Passo deve conter no máximo 10 caracteres");
            RuleFor(x => x.Consulta).GreaterThan(0).WithMessage("Consulta não informada");
        }

        #endregion

        private bool ValidarNull(BaseDomain model)
        {
            return model != null;
        }
    }
}
