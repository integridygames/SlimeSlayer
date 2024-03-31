using System;

namespace Game.Gameplay.TrashArchitecture.Commands
{
    public class SyncWaitCommand : ISpawnerCommand
    {
        public void Execute()
        {

        }

        public event Action<ISpawnerCommand> OnEnd;
        public int QueueIndex { get; }
    }
}