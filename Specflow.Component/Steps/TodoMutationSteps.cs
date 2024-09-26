using System;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Core.Enum;
using Service.Interface;
using TechTalk.SpecFlow;

namespace Specflow.Component.Steps
{
    [Binding]
    [Scope(Tag = "mutation")]
    public class TodoMutationSteps : SharedSteps
    {
        private readonly Mock<ITodoMutationService> _mockTodoMutationService = new();
        private readonly Mock<ITodoQueryService> _mockTodoQueryService = new();
        private Todo _todo;
        private int _todoId;

        [Given(@"the todo with id (.*) exists for user ""(.*)""")]
        public void GivenTheTodoWithIdExistsForUserInMutation(int todoId, string userId)
        {
            _todoId = todoId;
            UserId = userId;

            var mockTodo = new Todo { Id = todoId, Title = "Sample Todo", UserId = UserId };
            _mockTodoQueryService
                .Setup(s => s.GetTodoByTodoId(todoId, userId))
                .Returns(new List<Todo> { mockTodo }.AsQueryable());
        }

        [When(@"I add a todo with title ""(.*)"", description ""(.*)"", dueDate ""(.*)"", and priority ""(.*)""")]
        public async Task WhenIAddATodoWithDetails(string title, string description, DateTime dueDate, Priority priority)
        {
            _todo = new Todo
            {
                Title = title,
                Description = description,
                DueDateT = dueDate,
                Priority = priority,
                UserId = UserId,
                IsCompleted = false
            };

            _mockTodoMutationService.Setup(s => s.AddTodoAsync(It.IsAny<Todo>())).ReturnsAsync(_todo);

            await _mockTodoMutationService.Object.AddTodoAsync(_todo);
        }

        [Then(@"the todo should be added successfully")]
        public void ThenTheTodoShouldBeAddedSuccessfully()
        {
            Assert.IsNotNull(_todo);
            Assert.AreEqual(UserId, _todo.UserId);
        }

        [When(@"I mark the todo as completed")]
        public async Task WhenIMarkTheTodoAsCompleted()
        {
            var todo = _mockTodoQueryService.Object
                .GetTodoByTodoId(_todoId, UserId)
                .FirstOrDefault();
            
            if (todo != null)
            {
                todo.IsCompleted = true;

                _mockTodoMutationService
                    .Setup(s => s.UpdateTodoAsync(It.IsAny<Todo>()))
                    .ReturnsAsync(todo);

                await _mockTodoMutationService.Object.UpdateTodoAsync(todo);
                _todo = todo;
            }
        }

        [Then(@"the todo should be marked as completed")]
        public void ThenTheTodoShouldBeMarkedAsCompleted()
        {
            Assert.IsNotNull(_todo);
            Assert.IsTrue(_todo.IsCompleted);
        }

        [When(@"I delete the todo")]
        public async Task WhenIDeleteTheTodo()
        {
            var todos = _mockTodoQueryService.Object.GetTodoByTodoId(_todoId, UserId).ToList();

            _mockTodoMutationService
                .Setup(s => s.DeleteTodoAsync(It.IsAny<Todo>()))
                .ReturnsAsync(true)
                .Callback<Todo>(todo => todos.Remove(todo));

            var todoToDelete = todos.FirstOrDefault();
            var result = todoToDelete != null && await _mockTodoMutationService.Object.DeleteTodoAsync(todoToDelete);

            Assert.IsTrue(result);

            _mockTodoQueryService.Setup(s => s.GetTodoByTodoId(_todoId, UserId)).Returns(todos.AsQueryable());
        }

        [Then(@"the todo should be deleted successfully")]
        public void ThenTheTodoShouldBeDeletedSuccessfully()
        {
            var todo = _mockTodoQueryService.Object
                .GetTodoByTodoId(_todoId, UserId)
                .FirstOrDefault();
            
            Assert.IsNull(todo);
        }
    }
}