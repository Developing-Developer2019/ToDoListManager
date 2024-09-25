using Core.Enum;
using Core.Model;
using Data.Data;
using Service.Interface;

namespace Service.Service;

public class TodoService(ReadDbContext readDbContext) : ITodoService
{
    public IQueryable<Todo> GetTodosByUserId(string userId)
    {
        return readDbContext.Todo.Where(t => t.UserId == userId);
    }

    public IQueryable<Todo> GetTodoByTodoId(int id, string userId)
    {
        return readDbContext.Todo.Where(e => e.Id == id && e.UserId == userId);
    }

    public IQueryable<Todo> GetTodoByPriority(Priority priority)
    {
        return readDbContext.Todo.Where(e => e.Priority == priority);
    }
}