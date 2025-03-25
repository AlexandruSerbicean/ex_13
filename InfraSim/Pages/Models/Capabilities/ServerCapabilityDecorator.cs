namespace InfraSim.Pages.Models.Capabilities
{
    public abstract class ServerCapabilityDecorator : IServerCapability
    {
        protected readonly IServerCapability _capability;

        public ServerCapabilityDecorator(IServerCapability capability)
        {
            _capability = capability;
        }

        public abstract long MaximumRequests { get; }
        public abstract int Cost { get; }
    }
}
