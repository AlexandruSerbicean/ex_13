using System.Collections.Generic;
using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Pages.Models
{
    public class Cluster : BaseServer, ICluster
    {
        public Cluster(IServerCapability capability, IValidatorStrategy validator)
            : base(ServerType.Cluster, capability, validator) { }

        public List<IServer> Servers { get; set; } = new(); 
        public void AddServer(IServer server)    => Servers.Add(server);
        public void RemoveServer(IServer server) => Servers.Remove(server);
    }
}
