using System.Linq;
using InfraSim.Pages.Models;
public class GatewayValidator : IValidatorStrategy
{
    public bool Validate(IServer server)
    {
        if (server is not ICluster cluster) return false;

        return cluster.Servers.Any(s => s.ServerType == ServerType.CDN) &&
               cluster.Servers.Any(s => s.ServerType == ServerType.LoadBalancer);
    }
}