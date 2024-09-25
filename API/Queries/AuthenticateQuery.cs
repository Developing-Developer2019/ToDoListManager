using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace API.Queries;

public class AuthenticateQuery
{
    public string GenerateJwtToken(string userId, string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "7f532771-de9b-4185-af30-ba8b3d4a48d5-3f1735da-92e5-4408-87e3-b65c9d39b794"u8.ToArray();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}