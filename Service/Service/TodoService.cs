using Core.Model;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Service.Service;

public class TodoService : ITodoService
{
    private readonly ReadDbContext? _readDbContext;

    public TodoService(ReadDbContext? readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<List<Todo>> GetAllTodoAsync()
    {
        var todoList = await _readDbContext!.Todo.ToListAsync();
        return todoList;
    }

    public async Task<Todo> GetTodoById(int id)
    {
        var todo = await _readDbContext!.Todo.FirstOrDefaultAsync(e => e.Id == id);
        return todo ?? throw new Exception("Cannot find Todo with that ID");
    }
}