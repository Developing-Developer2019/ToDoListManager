using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace API.Authentication;

public static class UserIdFromToken
{
    public static string? GetUserIdFromToken(HttpContext httpContext)
    {
        var token = GetToken(httpContext);
        
        return !string.IsNullOrEmpty(token) ? 
            ExtractUserIdFromJwt(token) : 
            token;
    }

    private static string? GetToken(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader) &&
            authorizationHeader.ToString().StartsWith("Bearer "))
        {
            return authorizationHeader.ToString()["Bearer ".Length..];
        }

        return null;
    }

    private static string? ExtractUserIdFromJwt(string token)
    {
        try
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
            {
                return null;
            }
            
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub");

            return userIdClaim?.Value;

        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Invalid JWT token.", ex);
        }
    }
}