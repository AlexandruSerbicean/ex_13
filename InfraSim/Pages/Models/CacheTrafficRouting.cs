using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class CacheTrafficRouting : TrafficRouting
    {
        public CacheTrafficRouting(List<IServer> servers) : base(servers) { }

        public override void RouteTraffic(int requestsCount)
        {
            int requestsToCache = CalculateRequests(requestsCount);
            List<IServer> cacheServers = ObtainServers();
            SendRequestsToServers(requestsToCache, cacheServers);
        }

        public override int CalculateRequests(int requestsCount)
        {
            return (int)(requestsCount * 0.3); // 30% of requests go to cache
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == ServerType.Cache).ToList() ?? new List<IServer>();
        }

        public override void SendRequestsToServers(int requests, List<IServer> servers)
        {
            if (servers.Count == 0) return;  // ✅ Prevents division by zero

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
