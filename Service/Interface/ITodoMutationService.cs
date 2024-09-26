using Core.Model;

namespace Service.Interface;

public interface ITodoMutationService
{
    Task<Todo> AddTodoAsync(Todo todo);
    Task<Todo> UpdateTodoAsync(Todo todo);
    Task<bool> DeleteTodoAsync(Todo todo);
}