using FluentValidation;
using FluentValidation.Results;
using FluentValidation.WebApi;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace APIValidationDemo.Validation
{
    public class InputValidationFilter : ActionFilterAttribute
    {
        private IValidatorFactory _validatorFactory;

        public InputValidationFilter(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            foreach (var parameter in actionContext.ActionDescriptor.GetParameters())
            {
                object parameterValue;
                if (!parameter.IsOptional
                    && parameter.ParameterType.IsClass
                    && actionContext.ActionArguments.TryGetValue(parameter.ParameterName, out parameterValue))
                {
                    if (parameterValue == null)
                    {
                        AddNullModelError(parameter, actionContext.ModelState);
                    }
                    else 
                    {
                        var validators = _validatorFactory.GetValidators(parameter.ParameterType);

                        foreach (var validator in validators)
                        {
                            Validate(validator, parameterValue, actionContext.ModelState);
                        }
                    }
                }
            }
        }

        protected virtual void AddNullModelError(HttpParameterDescriptor parameter, ModelStateDictionary modelState)
        {
            modelState.AddModelError(parameter.ParameterName, string.Format("The {0} cannot be null.", parameter.ParameterName));
        }

        protected virtual void Validate(IValidator validator, object instance, ModelStateDictionary modelState)
        {
            ValidationResult result = validator.Validate(instance);

            if (!result.IsValid)
            {
                // https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation.WebApi/ValidationResultExtension.cs
                result.AddToModelState(modelState, null);
            }
        }
    }
}
