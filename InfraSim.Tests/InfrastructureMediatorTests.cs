using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Tests.MediatorTests
{
    public class InfrastructureMediatorTests
    {
        [Fact]
        public void AddServer_ShouldAddToGateway_IfServerIsCDN()
        {
            var factory = new ServerFactory(new ServerCapabilityFactory());
            var mediator = new InfrastructureMediator(factory);
            var server = factory.CreateServer(ServerType.CDN);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Gateway.Servers);
        }

        [Fact]
        public void AddServer_ShouldAddToProcessors_IfServerIsCache()
        {
            var factory = new ServerFactory(new ServerCapabilityFactory());
            var mediator = new InfrastructureMediator(factory);
            var server = factory.CreateServer(ServerType.Cache);

            mediator.AddServer(server);

            Assert.Contains(server, mediator.Processors.Servers);
        }

        [Fact]
        public void InfrastructureMediator_ShouldConnectGatewayWithProcessors()
        {
            var factory = new ServerFactory(new ServerCapabilityFactory());
            var mediator = new InfrastructureMediator(factory);

            Assert.Contains(mediator.Processors, mediator.Gateway.Servers);
        }
    }
}
