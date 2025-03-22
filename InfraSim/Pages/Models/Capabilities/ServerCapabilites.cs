namespace InfraSim.Pages.Models.Capabilities
{
    public interface IServerCapability
    {
        long MaximumRequests{ get; }
        int Cost { get; }
    }

    public class ServerCapability : IServerCapability
    {
        public long MaximumRequests => 1000;
        public int Cost => 2500;
    }
}
