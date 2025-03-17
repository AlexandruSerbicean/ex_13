using System.Collections.Generic;
using InfraSim.Pages.Models;
using Xunit;
using Moq;
using System.Linq;

public class CDNTrafficRoutingTests
{
    [Fact]
    public void CDNTrafficRouting_ShouldSend70PercentRequestsToCDN()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();
        var mockServer3 = new Mock<IServer>();

        // Setup server types
        mockServer1.Setup(s => s.ServerType).Returns(ServerType.CDN);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.CDN);
        mockServer3.Setup(s => s.ServerType).Returns(ServerType.Server); // Not a CDN server

        List<IServer> servers = new List<IServer> { mockServer1.Object, mockServer2.Object, mockServer3.Object };
        var routing = new CDNTrafficRouting(servers);

        int totalRequests = 100;
        int expectedCDNRequests = (int)(totalRequests * 0.7); // 70% of 100

        // Act
        routing.RouteTraffic(totalRequests);

        // Assert: Only CDN servers should receive traffic
        mockServer1.Verify(s => s.HandleRequests(expectedCDNRequests / 2), Times.Once);
        mockServer2.Verify(s => s.HandleRequests(expectedCDNRequests / 2), Times.Once);
        mockServer3.Verify(s => s.HandleRequests(It.IsAny<int>()), Times.Never); // Should receive 0
    }
}
