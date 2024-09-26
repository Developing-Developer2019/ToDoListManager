using Moq;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Service.Interface;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Specflow.Component.Steps
{
    [Binding]
    public class SharedSteps
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        protected readonly Mock<ITodoQueryService> MockTodoQueryService;
        protected string UserId;

        public SharedSteps()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext);

            MockTodoQueryService = new Mock<ITodoQueryService>();
        }

        [Given(@"the user with UserId ""(.*)"" exists")]
        public void GivenTheUserWithUserIdExists(string userId)
        {
            UserId = userId;
            SetupUserClaims(UserId);

            var mockTodos = new List<Todo>
            {
                new() { Id = 1, Title = "Test Todo 1", UserId = UserId },
                new() { Id = 2, Title = "Test Todo 2", UserId = UserId }
            };

            MockTodoQueryService.Setup(s => s.GetTodosByUserId(UserId))
                .Returns(mockTodos.AsQueryable());
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
    }
}