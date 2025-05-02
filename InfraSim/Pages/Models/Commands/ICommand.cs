namespace InfraSim.Pages.Models.Commands
{
    public interface ICommand
    {
        void Do();
        void Undo();
        void Redo();
    }
}
