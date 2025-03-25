using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Pages.Models
{
    public interface IServer
    {
        ServerType ServerType { get; }
        IServerCapability Capability { get; }
        int RequestsCount { get; set; }
        void HandleRequests(int requests);
    }

    public abstract class BaseServer : IServer
    {
        private int Requests = 0;

        public ServerType ServerType { get; private set; }
        public IServerCapability Capability { get; private set; }

        public int RequestsCount
        {
            get => Requests;
            set => Requests = value;
        }

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
