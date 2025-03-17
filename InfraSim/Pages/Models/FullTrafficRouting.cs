using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class FullTrafficRouting : TrafficRouting
    {
        public FullTrafficRouting(List<IServer> servers) : base(servers) { }

        public override int CalculateRequests(int requestsCount)
        {
            return requestsCount; 
        }

        public override List<IServer> ObtainServers()
        {
            return _servers; 
        }

        public override void SendRequestsToServers(int requests, List<IServer> servers)
        {
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
