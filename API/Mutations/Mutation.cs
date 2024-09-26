using API.Authentication;
using Core.Enum;
using Core.Model;
using Service.Interface;

namespace API.Mutations;

public class Mutation(
    ITodoQueryService todoQueryService, 
    ITodoMutationService todoMutationService, 
    IHttpContextAccessor httpContextAccessor)
{
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

    public async Task<Todo> AddTodo(string title, string description, DateTime dueDate, Priority priority)
    {
        var userId = GetUserId();
        
        var newTodo = new Todo
        {
            Title = title,
            Description = description,
            DueDateT = dueDate,
            Priority = priority,
            UserId = userId,
            IsCompleted = false
        };

        await todoMutationService.AddTodoAsync(newTodo);
        return newTodo;
    }

    public async Task<Todo> UpdateTodoAsCompleted(int todoId)
    {
        var userId = GetUserId();

        var ownsTodo = todoQueryService.DoesUserOwnTodo(todoId, userId);

        if (!ownsTodo)
        {
            throw new Exception($"Not authorized to update todo {todoId}");
        }
        
        var todo = todoQueryService.GetTodoByTodoId(todoId, userId).FirstOrDefault();
        
        if (todo == null)
        {
            throw new Exception($"Todo with id {todoId} not found.");
        }

        todo.IsCompleted = true;
        await todoMutationService.UpdateTodoAsync(todo);
        return todo;
    }

    public async Task<bool> DeleteTodoById(int todoId)
    {
        var userId = GetUserId();
        
        var ownsTodo = todoQueryService.DoesUserOwnTodo(todoId, userId);

        if (!ownsTodo)
        {
            throw new Exception($"Not authorized to update todo {todoId}");
        }
        
        var todo = todoQueryService.GetTodoByTodoId(todoId, userId).FirstOrDefault();
        
        if (todo == null)
        {
            throw new Exception($"Todo with id {todoId} not found.");
        }

        await todoMutationService.DeleteTodoAsync(todo);
        return true;
    }
}