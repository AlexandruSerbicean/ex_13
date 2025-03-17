using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class FullTrafficRouting : TrafficRouting
    {
        private readonly ServerType _serverType;

        public FullTrafficRouting(List<IServer> servers, ServerType serverType) : base(servers)
        {
            _serverType = serverType;
        }

        public override void RouteTraffic(int requestsCount)
        {
            int totalRequests = CalculateRequests(requestsCount);
            List<IServer> availableServers = ObtainServers();
            SendRequestsToServers(totalRequests, availableServers);
        }

        public override int CalculateRequests(int requestsCount)
        {
            return requestsCount; 
        }

        public override List<IServer> ObtainServers()
        {
            // Filter servers by the specified ServerType
            return _servers.Where(s => s.ServerType == _serverType).ToList();
        }

        public override void SendRequestsToServers(int requests, List<IServer> servers)
        {
            if (servers.Count == 0)
            {
                return; // No servers available to handle requests
            }

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
