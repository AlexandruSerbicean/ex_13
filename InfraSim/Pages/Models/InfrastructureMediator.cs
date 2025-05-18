using InfraSim.Pages.Models.Iterator;
using InfraSim.Pages.Models.Visitor;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.Commands;
using InfraSim.Pages.Models.Observer;
using InfraSim.Pages.Models.Traffic;

namespace InfraSim.Pages.Models
{
    public class InfrastructureMediator : IInfrastructureMediator, IObserver
    {
        public ICluster Gateway { get; private set; }
        public ICluster Processors { get; private set; }
        public ICluster Data { get; private set; }

        private readonly IServerDataMapper Mapper;
        private readonly ICommandManager CommandManager;

        public InfrastructureMediator(IServerFactory serverFactory, IServerDataMapper mapper, ICommandManager commandManager)
        {
            Mapper = mapper;
            CommandManager = commandManager;

            Gateway = serverFactory.CreateGatewayCluster();
            Processors = serverFactory.CreateProcessorsCluster();
            Data = serverFactory.CreateDataCluster();

            Processors.AddServer(Data);
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
                    break;

                case ServerType.Cache:
                case ServerType.Server:
                    addServerCommand = new AddServerCommand(Processors, server, Mapper);
                    break;

                case ServerType.Database:
                    addServerCommand = new AddServerCommand(Data, server, Mapper);
                    break;

                default:
                    return;
            }

            CommandManager.Execute(addServerCommand);
        }


        public IServerIterator CreateServerIterator()
        {
            return new ServerIterator(Gateway);
        }

        public int TotalCost
        {
            get
            {
                IServerIterator iterator = CreateServerIterator();
                var costCalculator = new CostCalculator();

                while (iterator.HasNext)
                    iterator.Next().Accept(costCalculator);

                return costCalculator.TotalCost;
            }
        }

        public bool IsOK
        {
            get
            {
                var iterator = CreateServerIterator();
                var statusCalculator = new StatusCalculator();

                while (iterator.HasNext)
                    iterator.Next().Accept(statusCalculator);

                return statusCalculator.IsOK;
            }
        }

        private ITrafficDelivery GetDeliveryChain()
        {
            ITrafficDelivery cdnChain = new CDNTrafficRouting(Gateway.Servers);
            ITrafficDelivery lbChain = new FullTrafficRouting(Gateway.Servers, ServerType.LoadBalancer);
            ITrafficDelivery cacheChain = new CacheTrafficRouting(Processors.Servers);
            ITrafficDelivery serverChain = new FullTrafficRouting(Processors.Servers, ServerType.Server);
            ITrafficDelivery databaseChain = new DatabaseTrafficRouting(Data.Servers);

            cdnChain.SetNext(lbChain);
            lbChain.SetNext(cacheChain);
            cacheChain.SetNext(serverChain);
            serverChain.SetNext(databaseChain);

            return cdnChain;
        }


        public void Update(int users)
        {
            long requestCount = users * 4L;
            ITrafficDelivery deliveryChain = GetDeliveryChain();
            deliveryChain.DeliverRequests(requestCount);
        }
        
    }
}
