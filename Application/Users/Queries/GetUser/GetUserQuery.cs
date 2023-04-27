using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserVm>
{
    public string Name { get; set; } = string.Empty;
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .Where(x => x.Name == request.Name)
            .ProjectTo<UserVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        return entity;
    }
}
