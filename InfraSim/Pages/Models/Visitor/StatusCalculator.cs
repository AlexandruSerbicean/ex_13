using InfraSim.Pages.Models.Visitor;

namespace InfraSim.Pages.Models.Visitor
{
    public class StatusCalculator : IServerVisitor
    {
        public bool IsOK { get; private set; } = true;

        public void Visit(IServer server)
        {
            IsOK = IsOK && server.Validator.Validate(server);
        }
    }
}
