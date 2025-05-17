using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class CacheTrafficRouting : TrafficRouting
    {
        public CacheTrafficRouting(List<IServer> servers) : base(servers) { }

        public override long CalculateRequests(long requestsCount)
        {
            return (int)(requestsCount * 0.8);
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == ServerType.Cache).ToList() ?? new List<IServer>();
        }
    }
}
