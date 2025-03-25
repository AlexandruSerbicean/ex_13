namespace InfraSim.Pages.Models.Capabilities
{
    public class TemporaryStorageDecorator : ServerCapabilityDecorator
    {
        public TemporaryStorageDecorator(IServerCapability capability) : base(capability) { }

        public override long MaximumRequests => _capability.MaximumRequests * 100;
        public override int Cost => _capability.Cost + 1000;
    }
}
