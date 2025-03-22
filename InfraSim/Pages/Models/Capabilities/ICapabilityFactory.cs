namespace InfraSim.Pages.Models.Capabilities
{
    public interface ICapabilityFactory
    {
        IServerCapability Create(ServerType type);
    }
}
