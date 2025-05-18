using System;

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
                ServerType.Cluster => new ServerCapability(),
                
                ServerType.Database     => new PersistentStorage(),

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Server type '{type}' is not supported.")
            };
        }
    }
}
