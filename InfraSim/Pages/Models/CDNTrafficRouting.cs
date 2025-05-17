using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class CDNTrafficRouting : TrafficRouting
    {
        public CDNTrafficRouting(List<IServer> servers) : base(servers) { }

        public override long CalculateRequests(long requestsCount)
        {
            return (int)(requestsCount * 0.7);
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == ServerType.CDN).ToList() ?? new List<IServer>();
        }
    }
}
