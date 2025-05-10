namespace InfraSim.Pages.Models.Visitor
{
    public interface IServerAcceptVisit
    {
        void Accept(IServerVisitor visitor);
    }
}