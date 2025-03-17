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

        public abstract void RouteTraffic(int requestsCount);
        public abstract int CalculateRequests(int requestsCount);
        public abstract List<IServer> ObtainServers();
        public abstract void SendRequestsToServers(int requests, List<IServer> servers);
    }
}
