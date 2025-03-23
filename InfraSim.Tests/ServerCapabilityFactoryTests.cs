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

    [Fact]
    public void Factory_ShouldThrowException_ForUnsupportedType()
    {
        var factory = new ServerCapabilityFactory();

        ServerType invalidType = (ServerType)999;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => factory.Create(invalidType));
        Assert.Contains("Server type", ex.Message);
    }

        [Fact]
    public void TemporaryStorageDecorator_ShouldDecorateProperly()
    {
        var baseCap = new ServerCapability();
        var decorator = new TemporaryStorageDecorator(baseCap);

        Assert.Equal(100000, decorator.MaximumRequests); 
        Assert.Equal(3500, decorator.Cost);              
    }


}