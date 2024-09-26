using API.Authentication;
using Core.Enum;
using Core.Model;
using Service.Interface;

namespace API.Queries;

public class Query(ITodoService todoService, IHttpContextAccessor httpContextAccessor)
{
    public IQueryable<Todo> GetTodosByUserId()
    {
        var userId = UserIdFromToken.GetUserIdFromToken(httpContextAccessor.HttpContext ?? throw new Exception("Can't find HttpContext"));
        if (httpContextAccessor.HttpContext?.User == null || userId == null)
        {
            throw new UnauthorizedAccessException("User not authorized or UserId not found.");
        }

        var todos = todoService.GetTodosByUserId(userId);
        if (todos == null || !todos.Any())
        {
            throw new Exception("No todos found for the user.");
        }

        return todos.AsQueryable();
    }

    public IQueryable<Todo> GetTodoByTodoId(int id)
    {
        
        var userId = UserIdFromToken.GetUserIdFromToken(httpContextAccessor.HttpContext ?? throw new Exception("Can't find HttpContext"));
        if (httpContextAccessor.HttpContext?.User == null || userId == null)
        {
            throw new UnauthorizedAccessException("User not authorized or TodoId not found.");
        }
        
        var todo = todoService.GetTodoByTodoId(id, userId);
        if (todo == null || !todo.Any())
        {
            throw new Exception("No todo found or unable to retrieve todo.");
        }

        return todo;
    }
    
    public IQueryable<Todo> GetTodoByTodoByPriority(Priority priority)
    {
        
        var userId = UserIdFromToken.GetUserIdFromToken(httpContextAccessor.HttpContext ?? throw new Exception("Can't find HttpContext"));
        if (httpContextAccessor.HttpContext?.User == null || userId == null)
        {
            throw new UnauthorizedAccessException("User not authorized or TodoId not found.");
        }
        
        var todo = todoService.GetTodoByPriority(priority, userId);
        if (todo == null || !todo.Any())
        {
            throw new Exception("No todo found or unable to retrieve todo.");
        }

        return todo;
    }
    
    public string GenerateJwtToken(string userId) => todoService.GetJwtToken(userId);
}