using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using Xunit;

public class ServerCapabilityFactoryTests
{
    [Theory]
    [InlineData(ServerType.Server, 1000, 2500)]
    [InlineData(ServerType.Cache, 100000, 3500)]
    [InlineData(ServerType.LoadBalancer, 10000000, 4000)]
    [InlineData(ServerType.CDN, 1000000000000, 55000)] 
    public void Factory_ShouldReturnCorrectCapability(ServerType type, long expectedRequests, int expectedCost)
    {
        var factory = new ServerCapabilityFactory();

        var capability = factory.Create(type);

        Assert.Equal(expectedRequests, capability.MaximumRequests);
        Assert.Equal(expectedCost, capability.Cost);
    }

}
