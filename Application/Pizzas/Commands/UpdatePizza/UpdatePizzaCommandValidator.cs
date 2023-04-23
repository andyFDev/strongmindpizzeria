using FluentValidation;

namespace Application.Pizzas.Commands.UpdatePizza;

public class UpdatePizzaCommandValidator : AbstractValidator<UpdatePizzaCommand>
{
    public UpdatePizzaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(128);

        RuleFor(x => x.Toppings)
            .NotEmpty();
    }
}
