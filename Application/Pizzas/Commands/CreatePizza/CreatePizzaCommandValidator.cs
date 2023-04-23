using FluentValidation;

namespace Application.Pizzas.Commands.CreatePizza;

public class CreatePizzaCommandValidator : AbstractValidator<CreatePizzaCommand>
{
    public CreatePizzaCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(128)
            .NotEmpty();

        RuleFor(x => x.Toppings)
            .NotEmpty();
    }
}
