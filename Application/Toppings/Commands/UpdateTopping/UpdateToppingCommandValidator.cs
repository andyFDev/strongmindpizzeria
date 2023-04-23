using FluentValidation;

namespace Application.Toppings.Commands.UpdateTopping;

public class UpdateToppingCommandValidator : AbstractValidator<UpdateToppingCommand>
{
    public UpdateToppingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(128);
    }
}
