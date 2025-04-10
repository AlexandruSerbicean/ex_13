using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models{
public class ServerFactory : IServerFactory
{
    private readonly ICapabilityFactory _capabilityFactory;

    public ServerFactory(ICapabilityFactory capabilityFactory)
    {
        _capabilityFactory = capabilityFactory;
    }

    public IServer CreateServer(ServerType type)
    {
        var capability = _capabilityFactory.Create(type);

        return new ServerBuilder()
                    .WithType(type)
                    .WithCapability(capability)
                    .WithState(new NormalState())
                    .Build();
    }

    public IServer CreateCache() => CreateServer(ServerType.Cache);

    public IServer CreateCDN() => CreateServer(ServerType.CDN);

    public IServer CreateLoadBalancer() => CreateServer(ServerType.LoadBalancer);

    public ICluster CreateCluster()
    {
        var capability = _capabilityFactory.Create(ServerType.Cluster);
        var cluster = new Cluster(capability);
        return cluster;
    }
}
}