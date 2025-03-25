using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public interface ICluster : IServer
    {
        void AddServer(IServer server);
        void RemoveServer(IServer server);
        List<IServer> Servers { get; }
    }
}
