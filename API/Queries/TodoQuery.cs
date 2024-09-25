using Core.Model;
using Service.Interface;

namespace API.Queries;

public class TodoQuery(ITodoService todoService)
{
    public IQueryable<Todo> GetTodos()
    {
        var todos = todoService.GetAllTodos();
        if (todos == null || !todos.Any())
        {
            throw new Exception("No todos found or unable to retrieve todos.");
        }

        return todos;
    }

    public IQueryable<Todo> GetTodoById(int id)
    {
        var todo = todoService.GetTodoById(id);
        if (todo == null || !todo.Any())
        {
            throw new Exception("No todo found or unable to retrieve todo.");
        }

        return todo;
    }
}