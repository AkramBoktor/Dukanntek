using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dukkantek.Domain.ViewModels.General;

namespace Dukkantek.Service.Validators
{
    class ValidatorHandler
    {
        public static List<Error> Validate<M>(M model, AbstractValidator<M> validator) where M : class
        {
            ValidationResult validationResult = validator.Validate(model);
            List<Error> errors = null;
            if (!validationResult.IsValid)
            {
                errors = ValidatorHandler.ErrorReturn(validationResult); ;
            }
            return errors;
        }
        private static List<Error> ErrorReturn(ValidationResult validationResult)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();
            foreach (var error in validationResult.Errors)
            {
                errors.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
            }
            return errors.Select(x => new Error() { ErrorMessage = x.ErrorMessage }).ToList();
        }
    }
}
