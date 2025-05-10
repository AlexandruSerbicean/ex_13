namespace InfraSim.Pages.Models.Iterator
{
    public interface IServerIterator
    {
        bool HasNext { get; }
        IServer Next();
    }
}
