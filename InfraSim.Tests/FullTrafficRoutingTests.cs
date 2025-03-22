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
        int result = routing.CalculateRequests(100);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void TestRequestCount_ShouldReturnCorrectValue()
    {
        var routing = new FullTrafficRouting(new List<IServer>(), ServerType.Server);
        Assert.Equal(50, routing.CalculateRequests(50));
        Assert.Equal(200, routing.CalculateRequests(200));
    }

    [Fact]
    public void TestSendRequestsToServers_ShouldDistributeRequestsEvenly()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();
        var mockServer3 = new Mock<IServer>();

        // Ensure mock servers return ServerType.Server
        mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer3.Setup(s => s.ServerType).Returns(ServerType.Server);

        List<IServer> servers = new List<IServer> { mockServer1.Object, mockServer2.Object, mockServer3.Object };
        var routing = new FullTrafficRouting(servers, ServerType.Server);

        // Act
        routing.SendRequestsToServers(100, servers);

        // Expected requests
        int expectedRequestsPerServer1 = 34;
        int expectedRequestsPerServer2 = 33;
        int expectedRequestsPerServer3 = 33;

        // Assert
        mockServer1.Verify(s => s.HandleRequests(expectedRequestsPerServer1), Times.Once);
        mockServer2.Verify(s => s.HandleRequests(expectedRequestsPerServer2), Times.Once);
        mockServer3.Verify(s => s.HandleRequests(expectedRequestsPerServer3), Times.Once);
    }

    [Fact]
    public void ObtainServers_ShouldReturnCorrectServers()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();

        // Ensure mock servers return ServerType.Server
        mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);
        mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);

        var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
        var routing = new FullTrafficRouting(servers, ServerType.Server);

        // Act
        var result = routing.ObtainServers();

        // Assert
        Assert.Equal(servers.Count, result.Count);
        Assert.Equal(servers, result);
    }   
}
