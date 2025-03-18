using System.Collections.Generic;
using InfraSim.Pages.Models;
using Xunit;
using Moq;
using System.Linq;

public class CacheTrafficRoutingTests
{
    [Fact]
    public void CacheTrafficRouting_ShouldSend80PercentRequestsToCache()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();
        var mockServer3 = new Mock<IServer>();

        // Setup server types
        mockServer1.Setup(s => s.ServerType).Returns(ServerType.Cache);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.Cache);
        mockServer3.Setup(s => s.ServerType).Returns(ServerType.Server); // Not a Cache server

        List<IServer> servers = new List<IServer> { mockServer1.Object, mockServer2.Object, mockServer3.Object };
        var routing = new CacheTrafficRouting(servers);

        int totalRequests = 100;
        int expectedCacheRequests = (int)(totalRequests * 0.8); 

        // Act
        routing.RouteTraffic(totalRequests);

        // Assert: Only Cache servers should receive traffic
        mockServer1.Verify(s => s.HandleRequests(expectedCacheRequests / 2), Times.Once);
        mockServer2.Verify(s => s.HandleRequests(expectedCacheRequests / 2), Times.Once);
        mockServer3.Verify(s => s.HandleRequests(It.IsAny<int>()), Times.Never);
    }
}
