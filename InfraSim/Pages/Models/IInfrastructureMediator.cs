using InfraSim.Pages.Models.Iterator;
namespace InfraSim.Pages.Models
{
    public interface IInfrastructureMediator
    {
        void AddServer(IServer server);
        ICluster Gateway { get; }
        ICluster Processors { get; }
        IServerIterator CreateServerIterator();
    }
}
