using InfraSim.Pages.Models.Visitor;

namespace InfraSim.Pages.Models.Visitor
{
    public class CostCalculator : IServerVisitor
    {
        public int TotalCost { get; private set; } = 0;

        public void Visit(IServer server)
        {
            TotalCost += server.Capability.Cost;
        }
    }
}
