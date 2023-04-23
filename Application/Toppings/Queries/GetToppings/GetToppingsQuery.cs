using Application.Common.Interfaces;
using Application.Common.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Toppings.Queries.GetToppings;

[Authorize(Role = "ToppingUser")]
public class GetToppingsQuery : IRequest<ToppingsVm>
{
}

public class GetToppingsQueryHandler : IRequestHandler<GetToppingsQuery, ToppingsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetToppingsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ToppingsVm> Handle(GetToppingsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Toppings
            .ProjectTo<ToppingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new ToppingsVm(entities);
    }
}
