using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Tests.BuilderTests
{
    public class ServerBuilderTests
    {
        [Fact]
        public void Build_ShouldCreateServer_WithGivenConfiguration()
        {
            // Arrange
            var type = ServerType.Cache;
            var capability = new TemporaryStorageDecorator(new ServerCapability());
            var state = new NormalState();

            var builder = new ServerBuilder();

            // Act
            var server = builder
                .WithType(type)
                .WithCapability(capability)
                .WithState(state)
                .Build();

            // Assert
            Assert.Equal(type, server.ServerType);
            Assert.Equal(capability.MaximumRequests, server.Capability.MaximumRequests);
            Assert.IsType<NormalState>(server.State);
        }
    }
}
