using InfraSim.Pages.Models.Database;

namespace InfraSim.Pages.Models.Commands
{
    public class AddServerCommand : ICommand
    {
        private readonly IServer _server;
        private readonly IServerList _proxy;

        public AddServerCommand(IServerList cluster, IServer server, IServerDataMapper mapper)
        {
            _server = server;
            _proxy = new ServerListProxy(cluster, mapper);
        }

        public void Do()
        {
            _proxy.AddServer(_server);
        }

        public void Undo()
        {
            _proxy.RemoveServer(_server);
        }

        public void Redo()
        {
            Do();
        }
    }
}
