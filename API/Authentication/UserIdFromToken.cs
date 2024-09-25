using System.Security.Claims;

namespace API.Authentication;

public static class UserIdFromToken
{
    public static string? GetUserIdFromToken(HttpContext httpContext)
    {
        // Try to get the 'sub' claim which contains the UserId
        var userId = httpContext.User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // Handle the case when no UserId is present (unauthorized or bad token)
            throw new UnauthorizedAccessException("The JWT token does not contain a valid UserId.");
        }

        return userId;
    }
    
    private static string? GetRawToken(HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers.Authorization.ToString();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader["Bearer ".Length..];
        }
        return null;
    }
}