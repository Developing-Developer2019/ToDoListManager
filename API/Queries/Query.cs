using API.Authentication;
using Core.Enum;
using Core.Model;
using Service.Interface;

namespace API.Queries;

public class Query(ITodoService todoService, IHttpContextAccessor httpContextAccessor)
{
    public IQueryable<Todo> GetTodosByUserId()
    {
        var userId = GetUserId();
        var todos = todoService.GetTodosByUserId(userId);
        return ValidateTodos(todos);
    }

    public IQueryable<Todo> GetTodoByTodoId(int id)
    {
        var userId = GetUserId();
        var todo = todoService.GetTodoByTodoId(id, userId);
        return ValidateTodos(todo);
    }

    public IQueryable<Todo> GetTodoByTodoByPriority(Priority priority)
    {
        var userId = GetUserId();
        var todo = todoService.GetTodoByPriority(priority, userId);
        return ValidateTodos(todo);
    }

    public string GenerateJwtToken(string userId) => todoService.GetJwtToken(userId);
    
    private string GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext ?? throw new Exception("Can't find HttpContext");
        var userId = UserIdFromToken.GetUserIdFromToken(httpContext);
        
        if (httpContext.User == null || userId == null)
        {
            throw new UnauthorizedAccessException("User not authorized or UserId not found.");
        }

        return userId;
    }

    private IQueryable<Todo> ValidateTodos(IQueryable<Todo> todos)
    {
        if (todos == null || !todos.Any())
        {
            throw new Exception("No todos found.");
        }

        return todos;
    }
}