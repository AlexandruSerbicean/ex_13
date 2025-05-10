namespace InfraSim.Pages.Models.Visitor
{
    public sealed class CostCalculator : IServerVisitor
    {
        public int TotalCost { get; private set; }

        public void Visit(IServer server)
        {
            TotalCost += server.Capability.Cost;
        }
    }
}