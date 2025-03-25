using InfraSim.Pages.Models.Health;

namespace InfraSim.Pages.Models.Health
{
    public class ServerHealthCheck : IServerHealthCheck
    {
        private readonly IServer _server;

        public ServerHealthCheck(IServer server)
        {
            _server = server;
        }

        public bool IsIdle => _server.RequestsCount == 0;
        public bool IsNormal => _server.RequestsCount > 0 && _server.RequestsCount < _server.Capability.MaximumRequests * 0.8;
        public bool IsOverloaded => _server.RequestsCount >= _server.Capability.MaximumRequests * 0.8
                                    && _server.RequestsCount < _server.Capability.MaximumRequests;
        public bool IsFailed => _server.RequestsCount >= _server.Capability.MaximumRequests;
    }
}
