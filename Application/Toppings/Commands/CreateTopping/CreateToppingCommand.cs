using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;

namespace Application.Toppings.Commands.CreateTopping;

[Authorize(Role = "ToppingManager")]
public class CreateToppingCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateToppingCommandHandler : IRequestHandler<CreateToppingCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateToppingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateToppingCommand request, CancellationToken cancellationToken)
    {
        if (_context.Toppings.Any(x => x.Name == request.Name))
        {
            throw new DuplicateException(nameof(Topping), request.Name!);
        }

        var entity = new Topping
        {
            Name = request.Name
        };

        _context.Toppings.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
