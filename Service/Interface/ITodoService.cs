using Core.Enum;
using Core.Model;

namespace Service.Interface;

public interface ITodoService
{
    IQueryable<Todo> GetAllTodos();
    IQueryable<Todo> GetTodoById(int id);
    IQueryable<Todo> GetTodoByPriority(Priority priority);
}