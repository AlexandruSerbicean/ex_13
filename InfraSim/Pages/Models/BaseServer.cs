using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public abstract class BaseServer : IServer
    {
        private int Requests = 0;

        public ServerType ServerType { get; private set; }
        public IServerCapability Capability { get; private set; }

        public int RequestsCount
        {
            get => Requests;
            set
            {
                Requests = value;
                State?.Handle(this);
            }
        }

        public IServerState State { get; set; }

        public virtual void HandleRequests(int requests)
        {
            RequestsCount = requests;
        }

        public BaseServer(ServerType type, IServerCapability capability)
        {
            ServerType = type;
            Capability = capability;
        }
    }
}
