namespace InfraSim.Pages.Models.Capabilities
{
    public class EdgeServerDecorator : ServerCapabilityDecorator
    {
        public EdgeServerDecorator(IServerCapability capability) : base(capability) { }

        public override long MaximumRequests => _capability.MaximumRequests * 1000;
        public override int Cost => _capability.Cost + 50000;
    }
}
