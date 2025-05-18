namespace InfraSim.Pages.Models.Capabilities
{
    public class PersistentStorage : IServerCapability
    {
        public long MaximumRequests => 100_000L * 100; 

        public int Cost => 13_000;
    }
}
