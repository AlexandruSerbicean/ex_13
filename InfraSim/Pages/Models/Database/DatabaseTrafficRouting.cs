using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models.Traffic
{
    public class DatabaseTrafficRouting : TrafficRouting
    {
        public DatabaseTrafficRouting(List<IServer> servers) : base(servers) { }

        public override long CalculateRequests(long requestCount)
        {
            return (long)(requestCount * 0.5); 
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == ServerType.Database).ToList() ?? new List<IServer>();
        }
    }
}
