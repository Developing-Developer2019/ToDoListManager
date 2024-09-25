using Core.Model;

namespace Service.Interface;

public interface ITodoService
{
    Task<List<Todo>> GetAllTodoAsync();
    Task<Todo> GetTodoById(int id);
}