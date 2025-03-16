using System.Collections.Generic;
using Xunit;


public class TrafficRoutingTests
{
    [Fact] 
    public void TestRequestCount_ShouldReturnCorrectRequestCount()
    {
        // Arrange
        TrafficRouting trafficRouting = new TrafficRouting(new List<IServer>());

        // Act
        int result = trafficRouting.CalculateRequests(100);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void TestRequestCount_ShouldReturnCorrectValue()
    {
        TrafficRouting trafficRouting = new TrafficRouting(new List<IServer>());
        Assert.Equal(50, trafficRouting.CalculateRequests(50));
        Assert.Equal(200, trafficRouting.CalculateRequests(200));
    }
}
