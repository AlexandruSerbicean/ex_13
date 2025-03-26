namespace InfraSim.Pages.Models.State
{
    public interface IServerState
    {
        void Handle(IServer server);
    }
}
