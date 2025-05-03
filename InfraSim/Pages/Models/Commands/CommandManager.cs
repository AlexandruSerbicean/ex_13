using System.Collections.Generic;

namespace InfraSim.Pages.Models.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly List<ICommand> Commands = new();
        private int Position = -1;

        public bool HasUndo => Position >= 0;
        public bool HasRedo => Position < Commands.Count - 1;

        public void Execute(ICommand command)
        {
            if (HasRedo)
            {
                Commands.RemoveRange(Position + 1, Commands.Count - Position - 1);
            }

            command.Do();
            Commands.Add(command);
            Position++;
        }

        public void Undo()
        {
            if (HasUndo)
            {
                Commands[Position].Undo();
                Position--;
            }
        }

        public void Redo()
        {
            if (HasRedo)
            {
                Position++;
                Commands[Position].Redo();
            }
        }
    }
}
