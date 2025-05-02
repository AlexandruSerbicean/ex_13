using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public interface ICluster : IServer, IServerList
    {
      List<IServer> Servers {get;}
      void AddServer(IServer server);
      void RemoveServer(IServer server);
    }
}
