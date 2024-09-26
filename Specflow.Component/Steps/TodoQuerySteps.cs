using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using Core.Enum;
using Core.Model;
using TechTalk.SpecFlow;

namespace Specflow.Component.Steps;

[Binding]
[Scope(Tag = "query")]
public class TodoQuerySteps : SharedSteps
{
    private IQueryable<Todo> _todos;
    private int _todoId;
    private Priority _priority;

    [When(@"I request todos by UserId")]
    public void WhenIRequestTodosByUserId()
    {
        _todos = MockTodoQueryService.Object.GetTodosByUserId(UserId).AsQueryable();
    }

    [Then(@"the todos for the user should be returned")]
    public void ThenTheTodosForTheUserShouldBeReturned()
    {
        Assert.IsNotNull(_todos);
        Assert.IsTrue(_todos.Any());
    }
    
    [Given(@"the todo with id (.*) exists for user ""(.*)""")]
    public void GivenTheTodoWithIdExistsForUserInQuery(int todoId, string userId)
    {
        _todoId = todoId;
        UserId = userId;

        var mockTodo = new Todo { Id = todoId, Title = "Sample Todo", UserId = UserId };
        MockTodoQueryService.Setup(s => s.GetTodoByTodoId(todoId, userId)).Returns(new List<Todo> { mockTodo }.AsQueryable());
    }

    [When(@"I request the todo by id")]
    public void WhenIRequestTheTodoById()
    {
        _todos = MockTodoQueryService.Object.GetTodoByTodoId(_todoId, UserId).AsQueryable();
    }

    [Then(@"the todo with id (.*) should be returned")]
    public void ThenTheTodoWithIdShouldBeReturned(int todoId)
    {
        Assert.IsNotNull(_todos);
        Assert.IsTrue(_todos.Any(t => t.Id == todoId));
    }

    [Given(@"the todos with priority ""(.*)"" exist for user ""(.*)""")]
    public void GivenTheTodosWithPriorityExistForUser(Priority priority, string userId)
    {
        _priority = priority;
        UserId = userId;

        var mockTodos = new List<Todo>
        {
            new() { Id = 1, Title = "High Priority Todo", Priority = priority, UserId = UserId }
        };

        MockTodoQueryService.Setup(s => s.GetTodoByPriority(priority, userId)).Returns(mockTodos.AsQueryable());
    }

    [When(@"I request the todos by priority")]
    public void WhenIRequestTheTodosByPriority()
    {
        _todos = MockTodoQueryService.Object.GetTodoByPriority(_priority, UserId).AsQueryable();
    }

    [Then(@"the todos with priority ""(.*)"" should be returned")]
    public void ThenTheTodosWithPriorityShouldBeReturned(Priority priority)
    {
        Assert.IsNotNull(_todos);
        Assert.IsTrue(_todos.Any(t => t.Priority == priority));
    }
}