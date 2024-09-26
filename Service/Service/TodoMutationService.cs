using Core.Model;
using Data.Data;
using Service.Interface;

namespace Service.Service;

public class TodoMutationService(WriteDbContext writeDbContext) : ITodoMutationService
{
    public async Task AddTodoAsync(Todo todo)
    {
        await writeDbContext.Todo.AddAsync(todo);
        await writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        writeDbContext.Todo.Update(todo);
        await writeDbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(Todo todo)
    {
        writeDbContext.Todo.Remove(todo);
        await writeDbContext.SaveChangesAsync();
    }
}