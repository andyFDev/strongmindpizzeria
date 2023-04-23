using Application.Common.Interfaces;
using Microsoft.Extensions.Primitives;

namespace WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => GetUserID();

    // This could be set by parsing a authorization token or something similar,
    // but for now is just a simple header look up of the id.
    private int? GetUserID()
    {
        var userId = _httpContextAccessor.HttpContext?.Request?.Headers["User"];

        if (userId.HasValue && int.TryParse(userId, out int value))
        {
            return value;
        }

        return null;
    }
}
