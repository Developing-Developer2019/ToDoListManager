using Core.Model;

namespace Service.Interface;

public interface ITodoMutationService
{
    Task AddTodoAsync(Todo todo);
    Task UpdateTodoAsync(Todo todo);
    Task DeleteTodoAsync(Todo todo);
}