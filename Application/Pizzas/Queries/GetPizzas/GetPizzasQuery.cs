using Application.Common.Interfaces;
using Application.Common.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Pizzas.Queries.GetPizzas;

[Authorize(Role = "PizzaUser")]
public class GetPizzasQuery : IRequest<PizzasVm>
{
}

public class GetPizzasQueryHandler : IRequestHandler<GetPizzasQuery, PizzasVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPizzasQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PizzasVm> Handle(GetPizzasQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Pizzas
            .Include(x => x.Toppings)
            .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PizzasVm(entities);
    }
}
