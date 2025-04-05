using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public class Server : BaseServer
    {
        public Server(ServerType type, IServerCapability capability, IServerState state)
            : base(type, capability)
        {
            State = state;
        }
    }
}
