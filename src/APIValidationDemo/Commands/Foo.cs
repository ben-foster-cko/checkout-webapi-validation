using FluentValidation;

namespace APIValidationDemo.Commands
{
    public class InvalidNameValidator : AbstractValidator<CreateItemCommand>
    {
        public InvalidNameValidator()
        {
            RuleFor(x => x.Name).NotEqual("ben");
        }
    }
}
