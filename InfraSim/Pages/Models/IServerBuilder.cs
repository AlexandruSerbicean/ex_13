using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public interface IServerBuilder
    {
        IServerBuilder WithType(ServerType type);
        IServerBuilder WithCapability(IServerCapability capability);
        IServerBuilder WithState(IServerState state);
        Server Build();
    }
}