using Microsoft.AspNetCore.Http;
using Moq;
using TechTalk.SpecFlow;

namespace Specflow.Component.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [BeforeScenario]
        public void SetUpHttpContext()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            
            var mockHttpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext);
            mockHttpContext.Request.Headers.Authorization = "Bearer sample_token";
        }

        public Mock<IHttpContextAccessor> GetMockHttpContextAccessor()
        {
            return _mockHttpContextAccessor;
        }
    }
}