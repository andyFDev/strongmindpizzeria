using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;

namespace Application.Toppings.Commands.DeleteTopping;

[Authorize(Role = "ToppingManager")]
public class DeleteToppingCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteToppingCommandHandler : IRequestHandler<DeleteToppingCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteToppingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteToppingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Toppings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Topping), request.Id);
        }

        _context.Toppings.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
