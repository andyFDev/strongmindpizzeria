using FluentValidation;

namespace Application.Toppings.Commands.CreateTopping;

public class CreateToppingCommandValidator : AbstractValidator<CreateToppingCommand>
{
    public CreateToppingCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(128)
            .NotEmpty();
    }
}
