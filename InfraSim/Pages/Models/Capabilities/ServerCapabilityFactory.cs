using System;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;

namespace InfraSim.Pages.Models.Capabilities
{
    public class ServerCapabilityFactory : ICapabilityFactory
    {
        public IServerCapability Create(ServerType type)
        {
            return type switch
            {
                ServerType.Server => new ServerCapability(),

                ServerType.Cache => new TemporaryStorageDecorator(
                                        new ServerCapability()),

                ServerType.LoadBalancer => new TrafficDistributionDecorator(
                                               new ServerCapability()),

                ServerType.CDN => new EdgeServerDecorator(
                                     new TrafficDistributionDecorator(
                                         new TemporaryStorageDecorator(
                                             new ServerCapability()))),

                ServerType.Cluster => new ServerCapability(), // ✅ Adaugă-l dacă e necesar pentru teste

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Server type '{type}' is not supported.")
            };
        }
    }
}
