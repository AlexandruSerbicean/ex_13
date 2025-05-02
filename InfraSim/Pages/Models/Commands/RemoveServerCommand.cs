using InfraSim.Pages.Models.Database;

namespace InfraSim.Pages.Models.Commands
{
    public class RemoveServerCommand : ICommand
    {
        private readonly IServer _server;
        private readonly IServerList _proxy;

        public RemoveServerCommand(IServerList cluster, IServer server, IServerDataMapper mapper)
        {
            _server = server;
            _proxy = new ServerListProxy(cluster, mapper);
        }

        public void Do()
        {
            _proxy.RemoveServer(_server);
        }

        public void Undo()
        {
            _proxy.AddServer(_server);
        }

        public void Redo()
        {
            Do();
        }
    }
}
