using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public abstract class TrafficRouting
    {
        protected List<IServer> _servers;

        public TrafficRouting(List<IServer> servers)
        {
            _servers = servers;
        }

        public void RouteTraffic(int requestsCount) 
        {
            int totalRequests = CalculateRequests(requestsCount);
            List<IServer> availableServers = ObtainServers();
            SendRequestsToServers(totalRequests, availableServers);
        }

        public abstract int CalculateRequests(int requestsCount); 
        public abstract List<IServer> ObtainServers(); 

        public void SendRequestsToServers(int requests, List<IServer> servers) 
        {
            if (servers.Count == 0) return; 

            int requestsPerServer = requests / servers.Count;
            int remainder = requests % servers.Count;

            foreach (var server in servers)
            {
                int assignedRequests = requestsPerServer + (remainder > 0 ? 1 : 0);
                server.HandleRequests(assignedRequests);
                remainder--;
            }
        }
    }
}
