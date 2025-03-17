using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public abstract class TrafficRouting
    {
        protected List<IServer> _servers;

        public TrafficRouting(List<IServer> servers)
        {
            _servers = servers;
        }

        //I will implement abstract methods in subclasses
        public abstract int CalculateRequests(int requestsCount);
        public abstract List<IServer> ObtainServers();
        public abstract void SendRequestsToServers(int requests, List<IServer> servers);
    }
}
