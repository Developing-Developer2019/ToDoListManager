using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Model;
using Core.Enum;
using Service.Interface;
using TechTalk.SpecFlow;

namespace Specflow.Component.Steps;

[Binding]
public class TodoQuerySteps
{
    private readonly Mock<ITodoService> _mockTodoService;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private IQueryable<Todo> _todos;
    private string _userId;
    private int _todoId;
    private Priority _priority;

    public TodoQuerySteps()
    {
        _mockTodoService = new Mock<ITodoService>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new DefaultHttpContext();
        _mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext);
    }

    [Given(@"the user with UserId ""(.*)"" exists")]
    public void GivenTheUserWithUserIdExists(string userId)
    {
        _userId = userId;
        SetupUserClaims(_userId);

        var mockTodos = new List<Todo>
        {
            new() { Id = 1, Title = "Test Todo 1", UserId = _userId },
            new() { Id = 2, Title = "Test Todo 2", UserId = _userId }
        };

        TodosForUser(_userId, mockTodos);
    }

    [When(@"I request todos by UserId")]
    public void WhenIRequestTodosByUserId()
    {
        _todos = _mockTodoService.Object.GetTodosByUserId(_userId).AsQueryable();
    }

    [Then(@"the todos for the user should be returned")]
    public void ThenTheTodosForTheUserShouldBeReturned()
    {
        Assert.IsNotNull(_todos);
        Assert.IsTrue(_todos.Any());
    }

    [Given(@"the todo with id (.*) exists for user ""(.*)""")]
    public void GivenTheTodoWithIdExistsForUser(int todoId, string userId)
    {
        _todoId = todoId;
        _userId = userId;
        SetupUserClaims(_userId);

        var mockTodo = new Todo { Id = todoId, Title = "Sample Todo", UserId = _userId };
        TodoById(_todoId, _userId, mockTodo);
    }

    [When(@"I request the todo by id")]
    public void WhenIRequestTheTodoById()
    {
        _todos = _mockTodoService.Object.GetTodoByTodoId(_todoId, _userId).AsQueryable();
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
        _userId = userId;
        SetupUserClaims(_userId);

        var mockTodos = new List<Todo>
        {
            new() { Id = 1, Title = "High Priority Todo", Priority = priority, UserId = _userId }
        };

        TodosByPriority(_priority, _userId, mockTodos);
    }

    [When(@"I request the todos by priority")]
    public void WhenIRequestTheTodosByPriority()
    {
        _todos = _mockTodoService.Object.GetTodoByPriority(_priority, _userId).AsQueryable();
    }

    [Then(@"the todos with priority ""(.*)"" should be returned")]
    public void ThenTheTodosWithPriorityShouldBeReturned(Priority priority)
    {
        Assert.IsNotNull(_todos);
        Assert.IsTrue(_todos.Any(t => t.Priority == priority));
    }
    
    private void SetupUserClaims(string userId)
    {
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }));

        var mockHttpContext = _mockHttpContextAccessor.Object.HttpContext;
        if (mockHttpContext != null) mockHttpContext.User = userClaims;
    }
    
    private void TodoById(int todoId, string userId, Todo todo)
    {
        _mockTodoService.Setup(s => s.GetTodoByTodoId(todoId, userId)).Returns(new List<Todo> { todo }.AsQueryable());
    }
    
    private void TodosForUser(string userId, List<Todo> todos)
    {
        _mockTodoService.Setup(s => s.GetTodosByUserId(userId)).Returns(todos.AsQueryable());
    }
    
    private void TodosByPriority(Priority priority, string userId, List<Todo> todos)
    {
        _mockTodoService.Setup(s => s.GetTodoByPriority(priority, userId)).Returns(todos.AsQueryable());
    }
}