using System.Collections.Generic;
using InfraSim.Pages.Models;
using Xunit;
using Moq;
public class FullTrafficRoutingTests
{
    [Fact]
    public void TestRequestCount_ShouldReturnCorrectRequestCount()
    {
        // Arrange
        var routing = new FullTrafficRouting(new List<IServer>(), ServerType.Server);

        // Act
        long result = routing.CalculateRequests(100);

        // Assert
        Assert.Equal(100L, result);
    }

    [Fact]
    public void TestRequestCount_ShouldReturnCorrectValue()
    {
        var routing = new FullTrafficRouting(new List<IServer>(), ServerType.Server);
        Assert.Equal(50L, routing.CalculateRequests(50));
        Assert.Equal(200L, routing.CalculateRequests(200));
    }

    [Fact]
    public void TestSendRequestsToServers_ShouldDistributeRequestsEvenly()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();
        var mockServer3 = new Mock<IServer>();

        mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer3.Setup(s => s.ServerType).Returns(ServerType.Server);

        List<IServer> servers = new List<IServer> { mockServer1.Object, mockServer2.Object, mockServer3.Object };
        var routing = new FullTrafficRouting(servers, ServerType.Server);


        routing.SendRequestsToServers(100, servers);

        int expectedRequestsPerServer1 = 34;
        int expectedRequestsPerServer2 = 33;
        int expectedRequestsPerServer3 = 33;

        mockServer1.Verify(s => s.HandleRequests(expectedRequestsPerServer1), Times.Once);
        mockServer2.Verify(s => s.HandleRequests(expectedRequestsPerServer2), Times.Once);
        mockServer3.Verify(s => s.HandleRequests(expectedRequestsPerServer3), Times.Once);
    }

    [Fact]
    public void ObtainServers_ShouldReturnCorrectServers()
    {
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();

        mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);

        var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
        var routing = new FullTrafficRouting(servers, ServerType.Server);

        var result = routing.ObtainServers();

        Assert.Equal(servers.Count, result.Count);
        Assert.Equal(servers, result);
    }
}
