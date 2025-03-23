using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TicketSystem.Application.ApplicationUser;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        var user = httpContextAccessor.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        var roles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        if (userId == null || email == null)
        {
            throw new InvalidOperationException("User identifier or email claim is missing");
        }

        return new CurrentUser(userId, email, roles);
    }
}