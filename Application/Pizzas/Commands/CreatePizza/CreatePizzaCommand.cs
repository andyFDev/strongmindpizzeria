using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using System.Data;

namespace Application.Pizzas.Commands.CreatePizza;

[Authorize(Role = "PizzaUser")]
public class CreatePizzaCommand : IRequest<int>
{
    public string Name { get; set; } = "";
    public List<string> Toppings { get; set; } = new List<string>();
}

public class CreatePizzaCommandHandler : IRequestHandler<CreatePizzaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePizzaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
    {
        if (_context.Pizzas.Any(x => x.Name == request.Name))
        {
            throw new DuplicateException(nameof(Topping), request.Name!);
        }

        var entity = new Pizza
        {
            Name = request.Name
        };

        var toppings = _context.Toppings.Where(x => request.Toppings.Contains(x.Name!));
        foreach (var topping in toppings)
        {
            entity.Toppings.Add(topping);
        }

        _context.Pizzas.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
