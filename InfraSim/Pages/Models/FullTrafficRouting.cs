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

        public override long CalculateRequests(long requestCount)
        {
            return requestCount;
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == _serverType).ToList() ?? new List<IServer>();
        }
    }
}
