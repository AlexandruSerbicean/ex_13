namespace InfraSim.Pages.Models.Capabilities
{
    public class TrafficDistributionDecorator : ServerCapabilityDecorator
    {
        public TrafficDistributionDecorator(IServerCapability capability) : base(capability) { }

        public override long MaximumRequests => _capability.MaximumRequests * 10000;
        public override int Cost => _capability.Cost + 1500;
    }
}
