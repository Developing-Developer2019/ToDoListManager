using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Enum;
using Core.Model;
using Data.Data;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;

namespace Service.Service;

public class TodoService(ReadDbContext readDbContext) : ITodoService
{
    public IQueryable<Todo> GetTodosByUserId(string userId) => 
        readDbContext.Todo.Where(t => t.UserId == userId);

    public IQueryable<Todo> GetTodoByTodoId(int id, string userId) => 
        readDbContext.Todo.Where(e => e.Id == id && e.UserId == userId);

    public IQueryable<Todo> GetTodoByPriority(Priority priority, string userId) =>
        readDbContext.Todo.Where(e => e.Priority == priority && e.UserId == userId);

    public string GetJwtToken(string userId) => GenerateJwtToken(userId);
    
    private static string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = EnvironmentSecrets.SecretKeyByte();

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