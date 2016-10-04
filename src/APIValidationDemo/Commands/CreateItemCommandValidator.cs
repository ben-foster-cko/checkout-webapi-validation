using FluentValidation;
using FluentValidation.Results;

namespace APIValidationDemo.Commands
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        ILogger logger;

        public CreateItemCommandValidator(ILogger logger)
        {
            // just to check we can resolve dependencies

            RuleFor(c => c.Amount)
                .LessThan(100)
                .WithMessage("The amount must be less than 100");
        }

        public override ValidationResult Validate(CreateItemCommand instance)
        {
            // Doesn't actually get executed, I guess this is due to datannotations

            return instance == null
                ? new ValidationResult(new[] { new ValidationFailure("Command", "Command cannot be null") })
                : base.Validate(instance);
        }
    }
}
