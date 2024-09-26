using Core.Model;
using Data.Data;
using Service.Interface;

namespace Service.Service;

public class TodoMutationService(WriteDbContext writeDbContext) : ITodoMutationService
{
    public async Task<Todo> AddTodoAsync(Todo todo)
    {
        var task = await writeDbContext.Todo.AddAsync(todo);
        await writeDbContext.SaveChangesAsync();
        return task.Entity;
    }

    public async Task<Todo> UpdateTodoAsync(Todo todo)
    {
       var task = writeDbContext.Todo.Update(todo);
        await writeDbContext.SaveChangesAsync();
        return task.Entity;
    }

    public async Task<bool> DeleteTodoAsync(Todo todo)
    {
        writeDbContext.Todo.Remove(todo);
        var result = await writeDbContext.SaveChangesAsync();
        return result > 0;
    }
}