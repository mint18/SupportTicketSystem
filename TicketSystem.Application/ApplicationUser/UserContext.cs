using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketSystem.Application.ApplicationUser;

namespace TicketSystem.Application.ApplicationUser;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated != true) return null;

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        var roles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        if (userId == null || email == null) return null;

        return new CurrentUser(userId, email, roles);
    }
}