using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public interface IServerList
    {
        List<IServer> Servers { get; set; }

        void AddServer(IServer server);

        void RemoveServer(IServer server);
    }
}
