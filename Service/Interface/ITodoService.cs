using Core.Enum;
using Core.Model;

namespace Service.Interface;

public interface ITodoService
{
    IQueryable<Todo> GetTodosByUserId(string userId);
    IQueryable<Todo> GetTodoByTodoId(int id, string userId);
    IQueryable<Todo> GetTodoByPriority(Priority priority);
}