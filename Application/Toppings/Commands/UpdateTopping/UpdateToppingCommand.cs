using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;

namespace Application.Toppings.Commands.UpdateTopping;

[Authorize(Role = "ToppingManager")]
public class UpdateToppingCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateToppingCommandHandler : IRequestHandler<UpdateToppingCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateToppingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateToppingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Toppings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Topping), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
