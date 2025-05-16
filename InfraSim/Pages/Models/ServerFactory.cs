using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.State;
using InfraSim.Pages.Models.Validator; 
using System.Linq;
using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public class ServerFactory : IServerFactory
    {
        private readonly ICapabilityFactory _capabilityFactory;
        private readonly IServerDataMapper? _mapper;

        public ServerFactory(ICapabilityFactory capabilityFactory, IServerDataMapper mapper)
        {
            _capabilityFactory = capabilityFactory;
            _mapper = mapper;
        }

        /* ---------- servers ---------- */

        public IServer CreateServer() => CreateServer(ServerType.Server);

        public IServer CreateServer(ServerType type) =>
            new ServerBuilder()
                .WithType(type)
                .WithCapability(_capabilityFactory.Create(type))
                .WithState(new NormalState())
                .WithValidator(new ServerValidator()) 
                .Build();

        public IServer CreateCache()        => CreateServer(ServerType.Cache);
        public IServer CreateCDN()          => CreateServer(ServerType.CDN);
        public IServer CreateLoadBalancer() => CreateServer(ServerType.LoadBalancer);

        /* ---------- empty cluster ---------- */

        public ICluster CreateCluster(IValidatorStrategy validator) =>
            new Cluster(_capabilityFactory.Create(ServerType.Cluster), validator);

        /* ---------- clusters loaded from DB ---------- */

        public ICluster CreateGatewayCluster()
        {
            var gateway = CreateCluster(new GatewayValidator());
            foreach (var s in GetAllSafe().Where(s => s.ServerType is ServerType.CDN or ServerType.LoadBalancer))
                gateway.AddServer(s);
            return gateway;
        }

        public ICluster CreateProcessorsCluster()
        {
            var processors = CreateCluster(new ProcessorsValidator());
            foreach (var s in GetAllSafe().Where(s => s.ServerType is ServerType.Cache or ServerType.Server))
                processors.AddServer(s);
            return processors;
        }

        /* ---------- helpers ---------- */

        private IEnumerable<IServer> GetAllSafe() =>
            _mapper?.GetAll() ?? Enumerable.Empty<IServer>();
    }
}
