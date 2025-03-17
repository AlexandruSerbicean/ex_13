using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class CDNTrafficRouting : TrafficRouting
    {
        public CDNTrafficRouting(List<IServer> servers) : base(servers) { }

        public override void RouteTraffic(int requestsCount)
        {
            int requestsToCDN = CalculateRequests(requestsCount);
            List<IServer> cdnServers = ObtainServers();
            SendRequestsToServers(requestsToCDN, cdnServers);
        }

        public override int CalculateRequests(int requestsCount)
        {
            return (int)(requestsCount * 0.7); // 70% of requests are sent to CDNs
        }

        public override List<IServer> ObtainServers()
        {
            return _servers.Where(s => s.ServerType == ServerType.CDN).ToList();
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
