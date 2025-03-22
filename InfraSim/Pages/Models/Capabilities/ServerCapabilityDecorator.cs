namespace InfraSim.Pages.Models.Capabilities
{
    public class ServerCapabilityDecorator : IServerCapability
    {
        protected readonly IServerCapability _capability;

        public ServerCapabilityDecorator(IServerCapability capability)
        {
            _capability = capability;
        }

        public virtual long MaximumRequests => _capability.MaximumRequests;
        public virtual int Cost => _capability.Cost;
    }
}
