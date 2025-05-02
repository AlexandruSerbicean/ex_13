using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Pages.Models
{
    public class InfrastructureMediator : IInfrastructureMediator
  {
        public ICluster Gateway { get; private set; }
        public ICluster Processors { get; private set; }
        private readonly IServerDataMapper Mapper;

    public InfrastructureMediator(IServerFactory serverFactory, IServerDataMapper dataMapper)
    {
        Mapper = dataMapper;

        Gateway = (ICluster)serverFactory.CreateCluster();
        Processors = (ICluster)serverFactory.CreateCluster();
        Gateway.AddServer(Processors);
    }

    public void AddServer(IServer server)
    {
        IServerList proxy;

        switch (server.ServerType)
        {
            case ServerType.CDN:
            case ServerType.LoadBalancer:
                proxy = new ServerListProxy(Gateway, Mapper);
                proxy.AddServer(server);
                break;

            case ServerType.Cache:
            case ServerType.Server:
                proxy = new ServerListProxy(Processors, Mapper);
                proxy.AddServer(server);
                break;
        }
    }
  }
}