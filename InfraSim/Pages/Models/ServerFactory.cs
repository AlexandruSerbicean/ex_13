using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.State;
using System.Linq;
using System.Collections.Generic;

namespace InfraSim.Pages.Models
{
    public class ServerFactory : IServerFactory
    {
        private readonly ICapabilityFactory _capabilityFactory;
        private readonly IServerDataMapper? _mapper;   // may be null!


        public ServerFactory(ICapabilityFactory capabilityFactory,
                             IServerDataMapper  mapper)
        {
            _capabilityFactory = capabilityFactory;
            _mapper            = mapper;
        }

        public ServerFactory(ICapabilityFactory capabilityFactory)
            : this(capabilityFactory, null!) { }

        /* ---------- single servers ---------- */

        public IServer CreateServer(ServerType type) =>
            new ServerBuilder()
                .WithType(type)
                .WithCapability(_capabilityFactory.Create(type))
                .WithState(new NormalState())
                .Build();

        public IServer CreateCache()        => CreateServer(ServerType.Cache);
        public IServer CreateCDN()          => CreateServer(ServerType.CDN);
        public IServer CreateLoadBalancer() => CreateServer(ServerType.LoadBalancer);

        /* ---------- empty cluster ---------- */

        public ICluster CreateCluster() =>
            new Cluster(_capabilityFactory.Create(ServerType.Cluster));

        /* ---------- clusters loaded from DB ---------- */

        public ICluster CreateGatewayCluster()
        {
            var gateway = CreateCluster();
            foreach (var s in GetAllSafe().Where(s => s.ServerType is ServerType.CDN
                                                                  or ServerType.LoadBalancer))
                gateway.AddServer(s);
            return gateway;
        }

        public ICluster CreateProcessorsCluster()
        {
            var processors = CreateCluster();
            foreach (var s in GetAllSafe().Where(s => s.ServerType is ServerType.Cache
                                                                  or ServerType.Server))
                processors.AddServer(s);
            return processors;
        }

        /* ---------- helpers ---------- */
        private IEnumerable<IServer> GetAllSafe() =>
            _mapper?.GetAll() ?? Enumerable.Empty<IServer>();
    }
}
