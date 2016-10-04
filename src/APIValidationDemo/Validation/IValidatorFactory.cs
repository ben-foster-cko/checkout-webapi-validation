using FluentValidation;
using System;
using System.Collections.Generic;

namespace APIValidationDemo.Validation
{
    public interface IValidatorFactory
    {
        IEnumerable<IValidator> GetValidators(Type typeToValidate);
    }
}
