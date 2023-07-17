using FluentValidation;
using Minsait.Examples.Application.Commands;

namespace Minsait.Examples.Application.Validators
{
    public class CreateMinsaitTestCommandValidator : AbstractValidator<CreateMinsaitTestCommand>
    {
        public CreateMinsaitTestCommandValidator()
        {
            ValidateValue();
        }

        private void ValidateValue()
        {
            RuleFor(obj => obj.Value)
                .Must(value => value > 0)
                .WithMessage("Value must be greater than 0");
        }
    }
}
