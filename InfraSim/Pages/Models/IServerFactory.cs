using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models

{
    public interface IServerFactory
    {
        IServer CreateServer(ServerType type);
        IServer CreateCluster();
    }

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

        public IServer CreateCluster()
        {
        var capability = _capabilityFactory.Create(ServerType.Cluster);
        return new Cluster(capability);
        }
    }
}
