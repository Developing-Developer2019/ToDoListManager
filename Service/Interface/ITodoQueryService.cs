using Core.Enum;
using Core.Model;

namespace Service.Interface;

public interface ITodoQueryService
{
    IQueryable<Todo> GetTodosByUserId(string userId);
    IQueryable<Todo> GetTodoByTodoId(int id, string userId);
    IQueryable<Todo> GetTodoByPriority(Priority priority, string userId);
    string GetJwtToken(string userId);
    bool DoesUserOwnTodo(int todoId, string userId);
}