using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;

namespace InfraSim.Tests.MediatorTests
{
    public class InfrastructureMediatorTests
    {
        private readonly IServerFactory factory;
        private readonly IServerDataMapper dataMapper;

        public InfrastructureMediatorTests()
        {
            var context = new MemoryInfraSimContext();
            context.Database.EnsureCreated();

            var capabilityFactory = new ServerCapabilityFactory();
            factory = new ServerFactory(capabilityFactory);

            var repoFactory = new RepositoryFactory(context);
            var unitOfWork = new UnitOfWork(context, repoFactory);
            dataMapper = new ServerDataMapper(unitOfWork, capabilityFactory);
        }

        [Fact]
        public void AddServer_ShouldAddToGateway_IfServerIsCDN()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper);
            var server = factory.CreateServer(ServerType.CDN);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Gateway.Servers);
        }

        [Fact]
        public void AddServer_ShouldAddToProcessors_IfServerIsCache()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper);
            var server = factory.CreateServer(ServerType.Cache);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Processors.Servers);
        }

        [Fact]
        public void InfrastructureMediator_ShouldConnectGatewayWithProcessors()
        {
            var mediator = new InfrastructureMediator(factory, dataMapper);

            Assert.Contains(mediator.Processors, mediator.Gateway.Servers);
        }
    }
}
