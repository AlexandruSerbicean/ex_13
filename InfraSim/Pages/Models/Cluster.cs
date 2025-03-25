using System.Collections.Generic;
using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Pages.Models
{
    public class Cluster : BaseServer, ICluster
    {
        private readonly List<IServer> _servers = new();

        public Cluster(IServerCapability capability)
            : base(ServerType.Cluster, capability)
        {
        }

        public void AddServer(IServer server)
        {
            _servers.Add(server);
        }

        public void RemoveServer(IServer server)
        {
            _servers.Remove(server);
        }

        public List<IServer> Servers => _servers;
    }
}
