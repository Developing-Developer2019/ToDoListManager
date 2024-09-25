using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Authentication;
using Core.Model;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;

namespace API.Queries;

public class Query(ITodoService todoService, IHttpContextAccessor httpContextAccessor)
{
    public IQueryable<Todo> GetTodosByUserId()
    {
        // Retrieve the userId from the token
        var userId = UserIdFromToken.GetUserIdFromToken(httpContextAccessor.HttpContext ?? throw new Exception("Can't find HttpContext"));

        if (httpContextAccessor.HttpContext?.User == null || userId == null)
        {
            throw new UnauthorizedAccessException("User not authorized or UserId not found.");
        }

        // Call service to get todos for the user
        var todos = todoService.GetTodosByUserId(userId);
        if (todos == null || !todos.Any())
        {
            throw new Exception("No todos found for the user.");
        }

        // Return the list of todos as IQueryable
        return todos.AsQueryable();
    }

    public IQueryable<Todo> GetTodoByTodoId(int id)
    {
        
        var userId = UserIdFromToken.GetUserIdFromToken(httpContextAccessor.HttpContext ?? throw new Exception("Can't find httpContext"));
        
        if (httpContextAccessor.HttpContext?.User == null || userId == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var todo = todoService.GetTodoByTodoId(id, userId);
        if (todo == null || !todo.Any())
        {
            throw new Exception("No todo found or unable to retrieve todo.");
        }

        return todo;
    }
    
    public string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "7f532771-de9b-4185-af30-ba8b3d4a48d5-3f1735da-92e5-4408-87e3-b65c9d39b794"u8.ToArray();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("sub", userId)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}