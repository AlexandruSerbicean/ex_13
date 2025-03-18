using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public class CDNTrafficRouting : TrafficRouting
    {
        public CDNTrafficRouting(List<IServer> servers) : base(servers) { }

        // ✅ Trebuie să rămână `public` pentru a respecta semnătura din clasa de bază
        public override int CalculateRequests(int requestsCount)
        {
            return (int)(requestsCount * 0.7); // ✅ 70% din trafic la CDN
        }

        public override List<IServer> ObtainServers()
        {
            return _servers?.Where(s => s.ServerType == ServerType.CDN).ToList() ?? new List<IServer>();
        }
    }
}
