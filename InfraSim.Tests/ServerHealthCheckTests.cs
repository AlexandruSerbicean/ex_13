
using Xunit;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Health;
using InfraSim.Pages.Models.State;

namespace InfraSim.Tests
{
    public class ServerHealthCheckTests
    {
        private class TestServer : BaseServer
        {
            public TestServer(IServerCapability capability) : base(ServerType.Server, capability) { }
        }

        [Theory]
        [InlineData(0, true, false, false, false)]
        [InlineData(500, false, true, false, false)] // 50%
        [InlineData(850, false, false, true, false)] // 85%
        [InlineData(1000, false, false, false, true)] // 100%
        public void HealthCheck_ShouldDetectCorrectState(int requestCount, bool isIdle, bool isNormal, bool isOverloaded, bool isFailed)
        {
            var server = new TestServer(new ServerCapability());
            server.HandleRequests(requestCount);

            var healthCheck = new ServerHealthCheck(server);

            Assert.Equal(isIdle, healthCheck.IsIdle);
            Assert.Equal(isNormal, healthCheck.IsNormal);
            Assert.Equal(isOverloaded, healthCheck.IsOverloaded);
            Assert.Equal(isFailed, healthCheck.IsFailed);
        }

        [Theory]
        [InlineData(0, typeof(IdleState))]
        [InlineData(500, typeof(NormalState))]
        [InlineData(900, typeof(OverloadedState))]
        [InlineData(1500, typeof(FailedState))]
        public void StateTransition_ShouldUpdateCorrectly(int requestCount, Type expectedState)
        {
            var server = new TestServer(new ServerCapability());
            server.State = new NormalState(); 
            server.HandleRequests(requestCount);

            Assert.IsType(expectedState, server.State);
        }
    }
}
