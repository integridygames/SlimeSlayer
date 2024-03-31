using System;

namespace Game.Gameplay.TrashArchitecture
{
    public interface ISpawnerCommand
    {
        public void Execute();

        public event Action<ISpawnerCommand> OnEnd;

        public int QueueIndex { get; }
    }
}