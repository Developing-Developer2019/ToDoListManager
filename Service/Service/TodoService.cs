using Core.Enum;
using Core.Model;
using Data.Data;
using Service.Interface;

namespace Service.Service;

public class TodoService : ITodoService
{
    private readonly ReadDbContext _readDbContext;

    public TodoService(ReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public IQueryable<Todo> GetAllTodos()
    {
        return _readDbContext.Todo;
    }

    public IQueryable<Todo> GetTodoById(int id)
    {
        return _readDbContext.Todo.Where(e => e.Id == id);
    }

    public IQueryable<Todo> GetTodoByPriority(Priority priority)
    {
        return _readDbContext.Todo.Where(e => e.Priority == priority);
    }
}