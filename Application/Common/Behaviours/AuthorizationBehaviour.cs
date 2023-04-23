using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }

            var user = await _context.Users
                        .Where(x => x.Id == _currentUserService.UserId)
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => a is not null);

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var role in authorizeAttributesWithRoles.Select(a => a.Role))
                {
                        var isInRole = user.Roles.Any(r => r.Name == role);
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
