using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Pizzas.Commands.UpdatePizza;

[Authorize(Role = "PizzaUser")]
public class UpdatePizzaCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<string> Toppings { get; set; } = new List<string>();
}

public class UpdatePizzaCommandHandler : IRequestHandler<UpdatePizzaCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePizzaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePizzaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Pizzas
            .Include(x => x.Toppings)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Pizza), request.Id);
        }

        entity.Name = request.Name;
        foreach (var toRemove in entity.Toppings.ToList())
        {
            entity.Toppings.Remove(toRemove);
        }

        var toppings = _context.Toppings.Where(x => request.Toppings.Contains(x.Name!));
        foreach (var toAdd in toppings)
        {
            entity.Toppings.Add(toAdd);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
