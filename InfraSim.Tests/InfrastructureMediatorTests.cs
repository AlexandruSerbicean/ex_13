using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.Commands;

namespace InfraSim.Tests.MediatorTests
{
    public class InfrastructureMediatorTests
    {
        private readonly IServerFactory factory;
        private readonly IServerDataMapper dataMapper;
        private readonly ICommandManager commandManager;

        public InfrastructureMediatorTests()
        {
            var context = new MemoryInfraSimContext();
            context.Database.EnsureCreated();

            var capabilityFactory = new ServerCapabilityFactory();
            factory = new ServerFactory(capabilityFactory, dataMapper);

            var repoFactory = new RepositoryFactory(context);
            var unitOfWork = new UnitOfWork(context, repoFactory);
            dataMapper = new ServerDataMapper(unitOfWork, capabilityFactory);

            commandManager = new CommandManager();
        }

        [Fact]
        public void AddServer_ShouldAddToGateway_IfServerIsCDN()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper, commandManager);
            var server = factory.CreateServer(ServerType.CDN);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Gateway.Servers);
        }

        [Fact]
        public void AddServer_ShouldAddToProcessors_IfServerIsCache()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper, commandManager);
            var server = factory.CreateServer(ServerType.Cache);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Processors.Servers);
        }

        [Fact]
        public void InfrastructureMediator_ShouldConnectGatewayWithProcessors()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper, commandManager);

            Assert.Contains(mediator.Processors, mediator.Gateway.Servers);
        }
    }
}
