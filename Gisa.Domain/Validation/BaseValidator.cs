using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Validation
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        #region [ Methods ]

        protected override bool PreValidate(ValidationContext<T> context, FluentValidation.Results.ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(typeof(T).Name, "Objeto não pode ser nulo"));
                return false;
            }
            return true;
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return context == null
            ? new ValidationResult(new[] { new ValidationFailure(typeof(T).Name, "Objeto não pode ser nulo") })
            : base.Validate(context);
        }

        #endregion
    }
}
