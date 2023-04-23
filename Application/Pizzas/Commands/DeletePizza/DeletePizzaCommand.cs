using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;

namespace Application.Pizzas.Commands.DeletePizza;

[Authorize(Role = "PizzaUser")]
public class DeletePizzaCommand : IRequest
{
    public int Id { get; set; }
}

public class DeletePizzaCommandHandler : IRequestHandler<DeletePizzaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePizzaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePizzaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Pizzas
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Pizza), request.Id);
        }

        _context.Pizzas.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
