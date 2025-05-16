using InfraSim.Pages.Models.Iterator;
using InfraSim.Pages.Models.Visitor;

namespace InfraSim.Pages.Models
{
    public interface IInfrastructureMediator
    {
        void AddServer(IServer server);
        ICluster Gateway { get; }
        ICluster Processors { get; }
        IServerIterator CreateServerIterator();
        bool IsOK { get; }
        int TotalCost { get; }   
    }
}
