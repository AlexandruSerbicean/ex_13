using System.Linq;
using InfraSim.Pages.Models;
public class ProcessorsValidator : IValidatorStrategy
{
    public bool Validate(IServer server)
    {
        if (server is not ICluster cluster) return false;

        return cluster.Servers.Any(s => s.ServerType == ServerType.Cache) &&
               cluster.Servers.Any(s => s.ServerType == ServerType.Server);
    }
}