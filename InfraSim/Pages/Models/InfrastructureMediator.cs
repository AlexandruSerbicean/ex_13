using InfraSim.Pages.Models.Iterator;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.Commands;

namespace InfraSim.Pages.Models
{
    public class InfrastructureMediator : IInfrastructureMediator
    {
        public ICluster Gateway { get; private set; }
        public ICluster Processors { get; private set; }

        private readonly IServerDataMapper Mapper;
        private readonly ICommandManager CommandManager;
        public IServerIterator CreateServerIterator()
            => new ServerIterator(Gateway);

        public InfrastructureMediator(IServerFactory serverFactory, IServerDataMapper dataMapper, ICommandManager commandManager)
        {
            Mapper = dataMapper;
            CommandManager = commandManager;

            Gateway = (ICluster)serverFactory.CreateCluster();
            Processors = (ICluster)serverFactory.CreateCluster();
            Gateway.AddServer(Processors);
        }

        public void AddServer(IServer server)
        {
            AddServerCommand addServerCommand;

            switch (server.ServerType)
            {
                case ServerType.CDN:
                case ServerType.LoadBalancer:
                    addServerCommand = new AddServerCommand(Gateway, server, Mapper);
                    CommandManager.Execute(addServerCommand);
                    break;

                case ServerType.Cache:
                case ServerType.Server:
                    addServerCommand = new AddServerCommand(Processors, server, Mapper);
                    CommandManager.Execute(addServerCommand);
                    break;
            }
        }
    }
}
