using FluentValidation;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIValidationDemo.Validation
{
    public class StructureMapValidatorFactory : IValidatorFactory
    {
        private readonly IContainer _container;

        public StructureMapValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<IValidator> GetValidators(Type typeToValidate)
        {
            var abstractValidatorType = typeof(IValidator<>);
            var validatorType = abstractValidatorType.MakeGenericType(typeToValidate);
            return _container.GetAllInstances(validatorType).Cast<IValidator>();      
        }
    }
}
